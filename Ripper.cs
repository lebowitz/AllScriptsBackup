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
        internal static CancellationToken Start(CancellationTokenSource tokenSource, Uri elasticSearchUri, string index, string type, IEnumerable<string> patientIds)
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
                var esConn = new ConnectionSettings(elasticSearchUri);
                esConn.SetDefaultIndex(index);
                
                var ec = new ElasticClient(esConn);
                                
                var findPatient = desktop.FindPatient();
                findPatient.ShowAllColumns();
                findPatient.SetSortBy("ID");

                foreach(string pid in patientIds.Select(i => StripNonAscii(i).Trim()))
                {
                    
                    var patient = ec.Get<JObject>(index, type, pid);

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
                        if (id == pid)
                        {
                            if (patient == null)
                            {
                                patient = FindPatientWindow.RowToPatient(findPatient.GetRow(1));                                
                            }

                            if (patient["demographics_html"] == null)
                            {
                                findPatient.Modify();
                                Thread.Sleep(250);
                                var demo = new PatientDemographicWindow();
                                demo.CloseChildWindows();
                                patient["demographics_html"] = demo.ToHtml(id);
                            }

                            ec.Index(patient, index, type, pid);                            
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
