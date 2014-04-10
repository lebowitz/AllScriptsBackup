using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Forms;

namespace AllScriptRipper
{
    internal class PrmWindow
    {
        internal AutomationElement Element { get; private set; }

        public PrmWindow(string startsWithTitle)
        {
            while (Element == null)
            {
                Element = AutomationElement.RootElement.GetWindows(startsWithTitle).FirstOrDefault();
                Thread.Sleep(125);
            }
        }

        public void CloseChildWindows()
        {
            foreach (AutomationElement w in Element.FindAll(TreeScope.Children, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Window)))
            {
                w.Close();
            }
        }

        public PrmWindow(AutomationElement automationElement)
        {
            Element = automationElement;
        }

        protected virtual AutomationElement GetBrowser()
        {
            return null;
        }

        public string ToHtml(string knownSubstring)
        {
            throw new NotImplementedException();
            Element.Focus();
            Thread.Sleep(250);
            string html = null;

            Clipboard.Clear();
            string[] requiredStrings = 
                (knownSubstring ?? string.Empty)
                .Trim()
                .Replace("(", " ")
                .Replace(")", " ")
                .Split(' ');

            while (string.IsNullOrEmpty(html) || !requiredStrings.All(rs => html.Contains(rs)))
            {
                var b = GetBrowser();

                if (b != null)
                {
                    var cp = b.Current.BoundingRectangle.TopLeft;
                    Cursor.Position = new System.Drawing.Point(Convert.ToInt32(cp.X)+5, Convert.ToInt32(cp.Y)+5);
                    UnmanagedHelper.mouse_event(UnmanagedHelper.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, new IntPtr());
                    UnmanagedHelper.mouse_event(UnmanagedHelper.MOUSEEVENTF_LEFTUP, 0, 0, 0, new IntPtr());
                }
                else
                {
                    Element.Focus();
                }

                Thread.Sleep(250);

                SendKeys.SendWait("^a");
                
                Thread.Sleep(250);

                SendKeys.SendWait("^c");
                
                Thread.Sleep(250);

                html = (string) Clipboard.GetData(DataFormats.Html);
            }

            html = HtmlHelpers.AddUtf8MetaTag(html);
            html = HtmlHelpers.RemoveHeaderLines(html);

            return html;
        }

        public Condition NotTopOrBottomCondition
        {
            get
            {
                return new NotCondition(new OrCondition(
                    new PropertyCondition(AutomationElement.AutomationIdProperty, "Top"),
                    new PropertyCondition(AutomationElement.AutomationIdProperty, "Bottom")));
            }
        }
        public AutomationElement ToolStripContainer
        {
            get
            {
                return Element.FindByIdPath("toolStripContainer");
            }
        }

        public void Close()
        {
            Thread.Sleep(150);
            
            Element.Close();

            Thread.Sleep(150);
        }

        public void Ok()
        {
            Element.FindByIdPath("_buttonOK").Invoke();
            Thread.Sleep(250);
        }
    }
}
