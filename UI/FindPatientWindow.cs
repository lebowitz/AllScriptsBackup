using System.Threading;
using System.Windows.Automation;

namespace AllScriptRipper
{
    internal class FindPatientWindow : PrmWindow
    {
        private AutomationElement _controlFind;

        public FindPatientWindow(AutomationElement e) : base(e)
        {}

        public void SetSortBy(string field)
        {
            Element
                .FindByIdPath("toolStripContainer/controlFind/mainMenu")
                .FindByNamePath("File/Reorder Columns")
                .Invoke();

            Thread.Sleep(250);

            Element
                .FindByIdPath("FormReorderSortColumns/toolStripContainer/spreadSort/_panelUserControls/0/findColumn/textbox_findColumn/rtftextbox_textbox_findColumn")
                .SetValue(field);

            Element
                .FindByIdPath("FormReorderSortColumns/toolStripContainer/_buttonOK")
                .Invoke();
        }

        public AutomationElement ControlFind
        {
            get { return _controlFind ?? (_controlFind = Element.FindByIdPath("toolStripContainer/controlFind")); }
        }

        private AutomationElement GetFirstRow()
        {
            return ControlFind
                .FindFirst(TreeScope.Descendants, 
                    new AndCondition(
                        new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Table), 
                        new PropertyCondition(AutomationElement.AutomationIdProperty, "spread")))
                .FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, "$1:$1"));
        }

        public string GetFirstRowId()
        {
            var firstRow = GetFirstRow();
            var cells = firstRow
                .FindAll(TreeScope.Descendants,
                    new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Edit));

            if (cells.Count > 0)
            {
                return cells[0].GetValue();
            }
            return null;
        }

        public void SetPatientId(string id)
        {
            AutomationElement textExternalId = ControlFind.FindByIdPath("panelControlFindBase/ControlFindPatient/textExternalId");

            //textExternalId.Focus();
            
            textExternalId.FindByIdPath("textBox_textExternalId").SetValue(id);
        }

        public void Search()
        {
            ControlFind
                .FindByIdPath("panelButtons/panelSearchClear/buttonSearch")
                .Invoke();
                Thread.Sleep(125);
        }

        public void Ok()
        {
            ControlFind
                .FindByIdPath("panelButtons/panelOkCancel/buttonOK")
                .Invoke();
        }

        public void Modify()
        {
            ControlFind
                .FindByIdPath("panelButtons/panelNewModifyDelete/buttonModify")
                .Invoke();
        }
    }
}
