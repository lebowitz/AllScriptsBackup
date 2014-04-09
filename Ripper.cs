using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Nest;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace AllScriptRipper
{
    static public class Ripper
    {
        internal static CancellationToken Start(CancellationTokenSource tokenSource, LoadOptions options)
        {
            CancellationToken ct = tokenSource.Token;
            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
          
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
                        desktop.CloseOtherPrmWindows();

                        findPatient.SetPatientId(pid);

                        findPatient.Search();

                        string id = StripNonAscii(findPatient.GetFirstRowId());
                        if (id == pid) // make sure the first row matches
                        {
                            if (patient == null) // new patient
                            {
                                patient = FindPatientWindow.RowToPatient(findPatient.GetRow(1));                                
                            }

                            PatientDemographicWindow demo = null;
                            if (options.Demographics || options.ReleaseOfInformation)
                            {
                                findPatient.Modify();
                                Thread.Sleep(250);
                                demo = new PatientDemographicWindow();
                                demo.CloseChildWindows();
                            }

                            if (options.Demographics)
                            {
                                patient["demographics_html"] = demo.ToHtml(id);                              
                            }

                            if (options.ReleaseOfInformation) {
                                var roi = demo.ReleaseInformation();
                                roi.Save();
                                roi.Close();
                                patient["released"] = true;
                            }

                            if (demo != null)
                            {
                                demo.Close();
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
        private static string StripNonAscii(string s)
        {
            return Regex.Replace(s, @"[^\u0000-\u007F]", string.Empty);
        }
    }
}
