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
            
            var html = new StringBuilder();
            
            html.Append("<head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"></meta></head>");

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
                                    o["dir"] = d.Name;
                                    o["title"] = o.Value<string>("title").Replace("Patient Demographics - ", "");
                                    var onePageSummaryDoc = new HtmlDocument {OptionFixNestedTags = true};
                                    onePageSummaryDoc.LoadHtml(o.Value<string>("one_page_summary_html"));

                                    var demographicsDoc = new HtmlDocument { OptionFixNestedTags = true };
                                    demographicsDoc.LoadHtml(o.Value<string>("demographics_html"));
                                    
                                    var remarks = o.Value<JArray>("remarks");
                                    string remarksStr = string.Empty;
                                    if (remarks != null)
                                    {
                                        remarksStr = "<h2>Remarks</h2><table><tr><th>Date</th><th>Remark</th></tr>";
                                        foreach (var r in remarks)
                                        {
                                            remarksStr += string.Format("<tr><td>{0}</td><td>{1}</td></tr>", r.Value<string>("Date"),
                                                r.Value<string>("Text"));
                                        }
                                        remarksStr += "</table>";
                                    }

                                    string opsInnerHtml = "<h2>One-Page Summary</h2>" + onePageSummaryDoc.DocumentNode.ChildNodes[2].ChildNodes[2].InnerHtml;
                                    string demoInnerHtml = "<h2>Demographics</h2>" + demographicsDoc.DocumentNode.ChildNodes[2].ChildNodes[2].InnerHtml;
                                    string rHtml = remarksStr + demoInnerHtml + opsInnerHtml;
                                    
                                    rHtml = "<body>" + rHtml + "</body>";

                                    rHtml = string.Format("<h1>{0}</h1>", o.Value<string>("title")) + rHtml;
                                    rHtml = rHtml.Replace("DISPLAY: none", string.Empty);
                                    rHtml = rHtml.Replace(@"style=""DISPLAY: none""", string.Empty);
                                    File.WriteAllText(Path.Combine(root.FullName, d.Name + ".html"), rHtml);
                                    
                                    return o;
                                }
                                

                                Trace.WriteLine("Missing patient.json in " + d.FullName);
                                return null;
                            }

                ).Where(o => o != null).OrderBy(l => l.Value<string>("name"));

            html.Append("<ul>");
            foreach (var l in links)
            {
                html.AppendFormat("<li><a href='{0}.html'>{1}</a></td></tr>", l.Value<string>("dir"), l.Value<string>("title")+"</li>");
            }
            html.Append("</ul>");
            
            File.WriteAllText(Path.Combine(root.FullName, "index.html"), html.ToString());
        }
    }
}
