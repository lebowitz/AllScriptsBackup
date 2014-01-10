using System.IO;
using System.Linq;
using System.Text;
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
                                o["id"] = d.Name;
                                o["bioLink"] = d.Name + "/bio.html";
                                o["onePageSummaryLink"] = d.Name + "/ops.html";

                                FileInfo[] files = d.GetFiles("name.txt");
                                if (files.Any())
                                {
                                    o["name"] = File.ReadAllText(files[0].FullName);
                                    return o;
                                }
                                else
                                {
                                    Directory.Delete(d.FullName);
                                    return null;
                                }

                            }

                ).Where(o => o != null).OrderBy(l => l.Value<string>("name"));

            foreach (var l in links)
            {
                html.AppendFormat("<tr><td>{2}</td><td><a href='{0}'>bio</a></td><td><a href='{1}'>summary</a></td></tr>", l.Value<string>("bioLink"), l.Value<string>("onePageSummaryLink"), l.Value<string>("name"));
            }
            html.Append("</table>");
            File.WriteAllText(Path.Combine(root.FullName, "index.html"), html.ToString());
        }
    }
}
