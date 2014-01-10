using System;
using System.Threading;
using System.Windows.Automation;

namespace AllScriptRipper
{
    internal class PrmDesktopWindow : PrmWindow
    {
        internal PrmDesktopWindow()
            : base("mmw_")
        { }

        internal void CloseOtherPrmWindows()
        {
            int processId = Element.Current.ProcessId;

            Func<AutomationElement, bool> keep = w => w.Current.AutomationId == Element.Current.AutomationId
                                                      || w.Current.Name == "Find Patient";
            
            var children = AutomationElement.RootElement.FindAll(TreeScope.Children,
                new AndCondition(
                    new PropertyCondition(AutomationElement.ProcessIdProperty, processId),
                    new PropertyCondition(AutomationElement.IsWindowPatternAvailableProperty, true)));
            foreach (AutomationElement w in children)
            {
                if (!keep(w))
                {
                    w.Close();
                }
            }

            foreach (var childWindow in Element.GetWindows())
            {
                if (!keep(childWindow))
                {
                    childWindow.Close();
                }
            }
        }

        internal FindPatientWindow FindPatient()
        {
            Element
                .FindByIdPath("toolStripContainer/Top/menuStrip")
                .FindByNamePath("Find Patient")
                .Invoke();

            AutomationElement findPatient = null;

            while (findPatient == null)
            {
                findPatient = Element.FindByNamePath("Find Patient");
                Thread.Sleep(125);
            }

            return new FindPatientWindow(findPatient);
        }
    }
}