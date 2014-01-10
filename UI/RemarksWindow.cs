using System.Collections.Generic;
using System.Globalization;
using System.Windows.Automation;

namespace AllScriptRipper
{
    internal class RemarksWindow : PrmWindow
    {
        public RemarksWindow() : base("New - Remarks")
        {
        }

        public RemarksWindow(AutomationElement automationElement) : base(automationElement)
        {
        }

        public List<Remark> GetAllRemarks()
        {

            var results = new List<Remark>();
            var puc = Element.FindByIdPath("_panelUserControls");
            int i = 0;
            while (true)
            {
                var g = puc.FindFirst(TreeScope.Children,
                    new PropertyCondition(AutomationElement.AutomationIdProperty, i.ToString(CultureInfo.InvariantCulture)));
                if (g == null)
                {
                    break;
                }

                var txt = (TextPattern)g.FindByIdPath("grpRemark/prmNote1/textBox/textContainer/textBox_textBox").GetCurrentPattern(TextPattern.Pattern);
                var r = new Remark()
                {
                    Date = g.FindByIdPath("grpRemark/prmGroupBox2").Current.Name,
                    Text = txt.DocumentRange.GetText(-1)
                };
                r.IsDemographic = g.FindByIdPath("grpRemark/prmGroupBox2/ckDemographicType").GetToggleState();
                r.IsBilling = g.FindByIdPath("grpRemark/prmGroupBox2/ckBillingType").GetToggleState();
                r.IsClinical = g.FindByIdPath("grpRemark/prmGroupBox2/ckClinicalType").GetToggleState();
                r.IsScheduling = g.FindByIdPath("grpRemark/prmGroupBox2/ckSchedulingType").GetToggleState();
                results.Add(r);
                i++;
            }

            return results;
        }
    }
}