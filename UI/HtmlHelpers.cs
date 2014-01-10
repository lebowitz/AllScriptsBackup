using System.Linq;

namespace AllScriptRipper
{
    internal class HtmlHelpers
    {
        internal static string AddUtf8MetaTag(string input)
        {
            return input.Replace("<HEAD>",
                "<HEAD><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"></meta>");
        }

        internal static string RemoveHeaderLines(string input)
        {
            return string.Join("\n", input.Split('\n').Skip(8));
        }
    }
}