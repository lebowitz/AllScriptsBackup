using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AllScriptRipper
{
    static public class Ripper
    {
        internal static CancellationToken Start(CancellationTokenSource tokenSource, DirectoryInfo dataDir, string idPrefix, string fromId, string toId)
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

                var findPatient = desktop.FindPatient();
                findPatient.SetSortBy("ID");

                string patientIdStr = string.Empty;
                for (int patientId = int.Parse(fromId); patientId <= int.Parse(toId); patientId++)
                {
                    if (ct.IsCancellationRequested)
                    {
                        Trace.WriteLine("Cancel requested...");
                        break;
                    }

                    try
                    {
                        patientIdStr = (idPrefix ?? string.Empty) + patientId.ToString(CultureInfo.InvariantCulture);

                        Trace.WriteLine(string.Format("Start Patient Id={0}:", patientIdStr));

                        desktop.CloseOtherPrmWindows();


                        findPatient.SetPatientId(patientIdStr);

                        findPatient.Search();

                        if (findPatient.GetFirstRowId() != patientIdStr)
                        {
                            Trace.WriteLine(string.Format("Skipping missing patient id '{0}'", patientIdStr));
                            continue;
                        }

                        var patientDir = CreatePatientDir(dataDir, patientIdStr);

                        findPatient.Modify();

                        var patientDemographic = new PatientDemographicWindow();

                        var patient = new JObject();

                        patient["title"] = patientDemographic.Title;
                        string name = patientDemographic.PatientNameFromTitle;

                        patient["name"] = name;
                     
                        var remarksWindow = patientDemographic.GetRemarksWindow();
                        
                        if (remarksWindow != null)
                        {
                            var remarks = remarksWindow.GetAllRemarks();
                            remarks.ForEach(r => Trace.WriteLine(string.Format("Remark [{0}]: {1}", r.Date, r.Text)));
                            patient["remarks"] = JArray.FromObject(remarks);
                            remarksWindow.Ok();
                        }

                        patient["demographics_html"] = patientDemographic.ToHtml(name);

                        var ops = patientDemographic.GetOnePageSummary();

                        ops.WaitForDoneStatus();

                        patient["one_page_summary_html"] = ops.ToHtml(name);

                        var patientJson = new FileInfo(Path.Combine(patientDir.FullName, "patient.json"));
                        Trace.WriteLine(string.Format("\tWriting {0}", patientJson.FullName));

                        File.WriteAllText(patientJson.FullName,
                            JsonConvert.SerializeObject(patient, Formatting.Indented));

                        patientDemographic.Close();
                        ops.Close();

                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine(ex.ToString());
                    }
                    finally
                    {
                        Trace.WriteLine(string.Format("End Patient Id={0}:", patientIdStr));
                    }
                }

                findPatient.Close();

                Indexer.Index(dataDir);
            }, ct, TaskCreationOptions.None, scheduler);

            return ct;
        }

        private static DirectoryInfo CreatePatientDir(DirectoryInfo dataDir, string patientIdStr)
        {
            var patientDir = new DirectoryInfo(Path.Combine(dataDir.FullName, patientIdStr));

            if (!patientDir.Exists)
            {
                patientDir.Create();
            }
            return patientDir;
        }
    }
}
