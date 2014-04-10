
using System.Linq;
using System.Threading;
using System.Windows.Automation;
using System.Windows.Forms;

namespace AllScriptRipper
{
    internal class PatientDemographicWindow : PrmWindow
    {
        internal PatientDemographicWindow()
            : base("Patient Demographics")
        {
        }

        public string Title
        {
            get
            {
                return Element.Current.Name;
            }
        }

        protected override AutomationElement GetBrowser()
        {
            return ToolStripContainer
                .FindFirst(TreeScope.Children, NotTopOrBottomCondition)
                .FindByIdPath("prmWebBrowser1");
        }

        public RemarksWindow GetRemarksWindow()
        {
            this.Element.Focus();
            SendKeys.SendWait("%pg");

            Thread.Sleep(125);

            var remarksDialog = Element.FindByNamePath("Remarks");
            if (remarksDialog != null)
            {
                var noRemarks = remarksDialog.FindByNamePath("No remarks found.");

                if (noRemarks != null)
                {
                    remarksDialog.FindByNamePath("OK").Invoke();
                    return null;
                }
            }

            SendKeys.SendWait("{enter}");

            AutomationElement e = Element.GetWindows("New - Remarks").FirstOrDefault();
            if (e != null)
            {
                return new RemarksWindow(e);
            }
            return null;
        }

        public string PatientNameFromTitle
        {
            get
            {
                return Title.Split('/')[0].Replace("Patient Demographics - ", string.Empty);
            }

        }

        internal void ReleaseInformation()
        {
            Element
                    .FindByIdPath("toolStripContainer/Top/PatientToolstrip")
                    .FindByNamePath("Release of Information...")
                    .Invoke();            
        }

        public OnePageSummaryWindow GetOnePageSummary()
        {
            Element
                .FindByIdPath("toolStripContainer/Top/PatientToolstrip")
                .FindByNamePath("OPS")
                .Invoke();

            AutomationElement ops = null;
            while (ops == null)
            {
                ops = AutomationElement.RootElement.GetWindows("One Page Summary").FirstOrDefault(); 
                Thread.Sleep(125);
            }

            return new OnePageSummaryWindow(ops);
        }
    }
}