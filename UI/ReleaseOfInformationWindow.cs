using System.Threading;
using System.Windows.Automation;

namespace AllScriptRipper
{
    internal class ReleaseOfInformationWindow : PrmWindow
    {
        public ReleaseOfInformationWindow() : base("Release of Information")
        {}

        public ReleaseOfInformationWindow(AutomationElement ae) : base(ae) { }

        internal void Save() 
        {
            Element.FindByIdPath("prmPanelPreview/btnPreview").Invoke();
            bool isSaveEnabled = false;
            Thread.Sleep(1000);

            while (!isSaveEnabled)
            {
                isSaveEnabled = GetSaveButton().Current.IsEnabled;
                Thread.Sleep(250);
            }

            Thread.Sleep(250);

            /*
            foreach (var w in AutomationElement.RootElement.GetWindows("File Download"))
            {
                w.Close();
            }*/

            GetSaveButton().Invoke();

            var browseForFolderWindow = Element.GetWindows("Browse For Folder")[0];
            browseForFolderWindow.FindByNamePath("OK").Invoke();

            var fileSavedWindow = Element.FindByNamePath("File Saved");
            fileSavedWindow
                .FindByIdPath("toolStripContainer")
                .FindFirst(TreeScope.Children, Condition.TrueCondition)
                .FindByIdPath("_panelFileSaved/_buttonOk").Invoke();

            // Name = 'File Download'
            // Type = 'ControlType.Window'

            // Name =  'Browse For Folder'
            // Type =  'ControlType.Window'
                // Name =  'OK'
                // Type =  'ControlType.Button'

            // Type = 'ControlType.Window'
            // Id = 'FormFileSaved'

        }

        AutomationElement saveButton = null;

        private AutomationElement GetSaveButton() 
        {
            if (saveButton == null)
            {
                saveButton = Element.FindByIdPath("prmPanelButtons/btnSaveClinicalData");
            }
            return saveButton;
        }

        
        internal void WaitForDoneStatus()
        {
            /*
            AutomationElement status = null;
            string statusValue = null;
            while (status == null || statusValue != "done")
            {
                status = Element.FindByIdPath("toolStripContainer/Bottom/statusStrip/StatusBar.Pane0");
                if (status != null)
                {
                    statusValue = ((ValuePattern)status.GetCurrentPattern(ValuePattern.Pattern)).Current.Value;
                }
            }*/
        }
    }
}