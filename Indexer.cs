using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;

namespace AllScriptRipper
{
    internal static class Indexer
    {
        internal static void Index(DirectoryInfo root)
        {
            var html = new StringBuilder("<table>");

            var links = root.GetDirectories()
                            .ToList()
                            .Select(d =>
                            {
                                JObject o = new JObject();
                                
                                FileInfo[] files = d.GetFiles("patient.json");
                                if (files.Any())
                                {
                                    Trace.WriteLine(string.Format("Added {0} to index...", d.FullName));
                                    o = JObject.Parse(File.ReadAllText(files[0].FullName));
                                    var onePageSummaryDoc = new HtmlDocument {OptionFixNestedTags = true};
                                    onePageSummaryDoc.LoadHtml(o.Value<string>("one_page_summary_html"));

                                    var demographicsDoc = new HtmlDocument { OptionFixNestedTags = true };
                                    demographicsDoc.LoadHtml(o.Value<string>("demographics_html"));
                                    
                                    var remarks = o.Value<JArray>("remarks");
                                    string remarksStr = string.Empty;
                                    if (remarks != null)
                                    {
                                        remarksStr = "<pre>" + remarks.ToString() + "</pre></br>";
                                    }

                                    string opsInnerHtml = onePageSummaryDoc.DocumentNode.ChildNodes[2].ChildNodes[2].InnerHtml;
                                    string demoInnerHtml = demographicsDoc.DocumentNode.ChildNodes[2].ChildNodes[2].InnerHtml;
                                    string overallHtml = remarksStr + demoInnerHtml + opsInnerHtml;

                                    overallHtml = "<HEAD><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"></meta>" + overallHtml;
                                    //htmlStr = htmlStr.Replace("Â", "");

                                    File.WriteAllText(Path.Combine(d.FullName, "patient.html"), overallHtml);
                                    
                                    return o;
                                }
                                

                                Trace.WriteLine("Missing patient.json in " + d.FullName);
                                return null;
                            }

                ).Where(o => o != null).OrderBy(l => l.Value<string>("name"));

            foreach (var l in links)
            {
                html.AppendFormat("<tr><td>{0}</td><td><a href='{1}'>bio</a></td><td><a href='{2}'>summary</a></td></tr>", l.Value<string>("title"), string.Empty, string.Empty);
            }
            html.Append("</table>");
                        
            File.WriteAllText(Path.Combine(root.FullName, "index.html"), html.ToString());
        }
    }
}
