using System.Windows.Automation;

namespace AllScriptRipper
{
    internal class OnePageSummaryWindow : PrmWindow
    {
        public OnePageSummaryWindow() : base("One Page Summary")
        {}

        public OnePageSummaryWindow(AutomationElement ae) : base(ae) { }

        protected override AutomationElement GetBrowser()
        {
            return ToolStripContainer
                .FindFirst(TreeScope.Children, NotTopOrBottomCondition)
                .FindByIdPath("prmPanel/Browser_summary");
        }

        internal void WaitForDoneStatus()
        {
            AutomationElement status = null;
            string statusValue = null;
            while (status == null || statusValue != "done")
            {
                status = Element.FindByIdPath("toolStripContainer/Bottom/statusStrip/StatusBar.Pane0");
                if (status != null)
                {
                    statusValue = ((ValuePattern)status.GetCurrentPattern(ValuePattern.Pattern)).Current.Value;
                }
            }
        }
    }
}