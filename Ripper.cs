using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Automation;
using Nest;
using Newtonsoft.Json.Linq;

namespace AllScriptRipper
{
    static public class Ripper
    {
        internal static CancellationToken Start(CancellationTokenSource tokenSource, LoadOptions options)
        {
            CancellationToken ct = tokenSource.Token;
            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();

            Task.Factory.StartNew(() => ExceptionHunterKiller(ct), ct);
          
            Task.Factory.StartNew(() =>
            {
                var desktop = new PrmDesktopWindow();
                desktop.Element.Focus();

                foreach (var childWindow in desktop.Element.GetWindows())
                {
                    childWindow.Close();
                }
                var esConn = new ConnectionSettings(options.ElasticSearchUri);
                esConn.SetDefaultIndex(options.Index);
                
                var ec = new ElasticClient(esConn);
                                
                var findPatient = desktop.FindPatient();
                findPatient.ShowAllColumns();
                findPatient.SetSortBy("ID");

                foreach(string pid in options.PatientIds.Select(i => StripNonAscii(i).Trim()))
                {
                    var patient = ec.Get<JObject>(options.Index, options.Type, pid);

                    if (ct.IsCancellationRequested)
                    {
                        Trace.WriteLine("Cancel requested...");
                        break;
                    }

                    try
                    {
                        desktop.CloseAllWindowsButFind();

                        findPatient.SetPatientId(pid);

                        findPatient.Search();

                        string id = StripNonAscii(findPatient.GetFirstRowId());
                        if (id == pid) // make sure the first row matches
                        {
                            if (patient == null) // new patient
                            {
                                patient = FindPatientWindow.RowToPatient(findPatient.GetRow(1));                                
                            }

                            findPatient.Modify();
                            Thread.Sleep(250);
                            var demo = new PatientDemographicWindow();
                            demo.CloseChildWindows();

                            if (options.Demographics)
                            {
                                patient["demographics_html"] = demo.ToHtml(id);
                            }

                            if (options.ReleaseOfInformation) {
                                demo.ReleaseInformation();
                                var roi = new ReleaseOfInformationWindow();
                                roi.Save();

                                roi.Close();
                                patient["released"] = true;
                            }

                            

                            ec.Index(patient, options.Index, options.Type, pid);                            
                        }
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine(ex.ToString());
                    }
                }

                findPatient.Close();                
            }, ct, TaskCreationOptions.None, scheduler);

            return ct;
        }

        private static void ExceptionHunterKiller(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                Thread.Sleep(TimeSpan.FromSeconds(10));

                int pid = Process.GetProcessesByName("iMedica.Prm.Client").First().Id;
                var rootWindows = AutomationElement.RootElement.FindAll(TreeScope.Children,
                    new PropertyCondition(AutomationElement.ProcessIdProperty, pid));

                foreach (AutomationElement w in rootWindows)
                {
                    if (CloseExceptionWindow(w))
                    {
                        var desktop = new PrmDesktopWindow();
                        desktop.CloseAllWindowsButFind();                        
                    }

                    var formProcessing = w.FindFirst(TreeScope.Children,
                        new PropertyCondition(AutomationElement.AutomationIdProperty, "FormProcessingStatus"));

                    if (formProcessing != null)
                    {
                        var tw = TreeWalker.ControlViewWalker;
                        var formProcessingParent = tw.GetParent(formProcessing);
                        var formProcessingParentWindow =
                            ((WindowPattern) formProcessingParent.GetCurrentPattern(WindowPattern.Pattern));

                        var errorDialogs = formProcessing.FindAll(TreeScope.Children,
                            new PropertyCondition(AutomationElement.LocalizedControlTypeProperty, "Dialog"));
                        if (errorDialogs.Count > 0)
                        {
                            var errorDialog = errorDialogs[0];
                            var okButton = errorDialog.FindFirst(TreeScope.Children,
                                new PropertyCondition(AutomationElement.NameProperty, "OK"));
                            if (okButton != null)
                            {
                                ((InvokePattern) okButton.GetCurrentPattern(InvokePattern.Pattern)).Invoke();
                                Thread.Sleep(250);
                            }
                        }

                        if (CloseExceptionWindow(formProcessing))
                        {
                            formProcessingParentWindow.Close();
                        }
                    }
                }
            }
        }

        private static bool CloseExceptionWindow(AutomationElement e)
        {
            var exceptionWindow = e.FindFirst(TreeScope.Children,
                            new PropertyCondition(AutomationElement.AutomationIdProperty, "ExceptionUI"));
            if (exceptionWindow != null)
            {
                var ignoreButton = exceptionWindow.FindFirst(TreeScope.Children,
                    new PropertyCondition(AutomationElement.AutomationIdProperty, "buttonIgnore"));
                if (ignoreButton != null)
                {
                    ((InvokePattern)ignoreButton.GetCurrentPattern(InvokePattern.Pattern)).Invoke();
                    Thread.Sleep(250);
                    return true;
                }
            }
            return false;
        }


        private static string StripNonAscii(string s)
        {
            return Regex.Replace(s, @"[^\u0000-\u007F]", string.Empty);
        }
    }
}
