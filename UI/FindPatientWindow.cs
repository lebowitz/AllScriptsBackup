using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows.Automation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AllScriptRipper
{
    internal class FindPatientWindow : PrmWindow
    {
        private AutomationElement _controlFind;
        private AutomationElement _spreadSheetTable;
        private AutomationElement _controlFindPatient;

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

            var toolStripContainer = Element.FindByIdPath("FormReorderSortColumns/toolStripContainer");

            var pane = toolStripContainer.FindFirst(TreeScope.Children,
                new NotCondition(new PropertyCondition(AutomationElement.NameProperty, "Bottom")));
            pane.FindFirst(TreeScope.Children,
                new PropertyCondition(AutomationElement.AutomationIdProperty, "_buttonOK")).Invoke();           
        }
        public void ShowAllColumns()
        {
            Element
                .FindByIdPath("toolStripContainer/controlFind/mainMenu")
                .FindByNamePath("File/Reorder Columns")
                .Invoke();

            Thread.Sleep(250);

            var toolStripContainer = Element.FindByIdPath("FormReorderSortColumns/toolStripContainer");

            toolStripContainer.FindByIdPath("buttonClearAll").Invoke();
            var pane = toolStripContainer.FindFirst(TreeScope.Children,
                new NotCondition(new PropertyCondition(AutomationElement.NameProperty, "Bottom")));
            pane.FindFirst(TreeScope.Children,
                new PropertyCondition(AutomationElement.AutomationIdProperty, "_buttonOK")).Invoke();         
        }

        public AutomationElement ControlFind
        {
            get { return _controlFind ?? (_controlFind = Element.FindByIdPath("toolStripContainer/controlFind")); }
        }

        public AutomationElement GetRow(int i)
        {
            return GetSpreadsheetTable()
                .FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, "$"+i+":$"+i));
        }

        public static JObject RowToPatient(AutomationElement row)
        {
            var p = new JObject();
            
            var cells = row.FindAll(TreeScope.Children, Condition.TrueCondition);
            var columns = new[]
            {
                "ID",
                "Name",
                "AKAName",
                "FirstName",
                "LastName",
                "AKAFirstName",
                "AKALastName",
                "SSN",
                "MRN",
                "PrimaryPhone",
                "PrimaryPhone2",
                "Address1",
                "Address2",
                "City",
                "State",
                "Zip",
                "Notes",
                "CommunityId",
            };
            int i = 0;
            foreach (AutomationElement c in cells)
            {
                string value = null;
                try
                {
                     value = c.FindFirst(TreeScope.Children, Condition.TrueCondition).GetValue();
                     p[columns[i]] = value;
                     i++;
                }
                catch (Exception ex)
                {
                }
                
            }
        
            return p;
        }

        public IEnumerable<JObject> GetAllRows()
        {
            var result = new List<JObject>();

            var rows = GetSpreadsheetTable().FindAll(TreeScope.Children, Condition.TrueCondition);
            
            foreach (AutomationElement r in rows)
            {
                if (r.Current.Name.Contains("$"))
                {
                    var patientRow = RowToPatient(r);
                    result.Add(patientRow);
                    Trace.WriteLine(patientRow.ToString(Formatting.Indented));
                }
            }           
            return result;
        }

        public AutomationElement GetSpreadsheetTable()
        {
            if (_spreadSheetTable == null)
            {
                 _spreadSheetTable = ControlFind
                .FindFirst(TreeScope.Descendants, 
                    new AndCondition(
                        new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Table), 
                        new PropertyCondition(AutomationElement.AutomationIdProperty, "spread")));;
            }

            return _spreadSheetTable;
        }

        public string GetFirstRowId()
        {
            try
            {
                var firstRow = GetRow(1);
                var patient = RowToPatient(firstRow);

                if (patient != null)
                {
                    return patient.Value<string>("ID").Trim();
                }
            }
            catch (Exception)
            {
                return null;
            }

            return null;
        }

        public AutomationElement GetControlFindPatient()
        {
            if (_controlFindPatient != null) return _controlFindPatient;
            _controlFindPatient = ControlFind.FindByIdPath("panelControlFindBase/ControlFindPatient");
            return _controlFindPatient;
        }

        public void SetPatientId(string id)
        {
            AutomationElement textExternalId = GetControlFindPatient().FindByIdPath("textExternalId");
            textExternalId.FindByIdPath("textBox_textExternalId").SetValue(id);
        }
        public void SetLastName(string lastName)
        {
            AutomationElement textExternalId = GetControlFindPatient().FindByIdPath("textLastName");
            textExternalId.FindByIdPath("textBox_textLastName").SetValue(lastName);
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

        public void SetCommunityId(string value)
        {
            AutomationElement ae = GetControlFindPatient().FindByIdPath("_textCommunityId/textContainer/textBox__textCommunityId");
            ae.SetValue(value);
        }
    }
}
