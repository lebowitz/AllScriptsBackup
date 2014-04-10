using System.Threading;
using System.Windows.Automation;

namespace AllScriptRipper
{
    internal class ReleaseOfInformationWindow : PrmWindow
    {
        public ReleaseOfInformationWindow() : base("Release of Information")
        {}

        public ReleaseOfInformationWindow(AutomationElement ae) : base(ae) { }

        private AutomationElement PreviewElement()
        {
            return Element.FindByIdPath("prmPanelPreview/btnPreview");
        }

        internal void Save() 
        {
            Thread.Sleep(250);

            PreviewElement().Invoke();
            bool isSaveEnabled = false;
            
            while (!isSaveEnabled)
            {
                Thread.Sleep(250);
                isSaveEnabled = GetSaveButton().Current.IsEnabled;                
            }

            Thread.Sleep(250);

            /*
            foreach (var w in AutomationElement.RootElement.GetWindows("File Download"))
            {
                w.Close();
            }*/

            GetSaveButton().Invoke();

            Thread.Sleep(250);

            var browseForFolderWindow = Element.GetWindows("Browse For Folder")[0];
            browseForFolderWindow.FindByNamePath("OK").Invoke();

            Thread.Sleep(250);

            var fileSavedWindow = Element.FindByNamePath("File Saved");
            fileSavedWindow
                .FindByIdPath("toolStripContainer")
                .FindFirst(TreeScope.Children, Condition.TrueCondition)
                .FindByIdPath("_panelFileSaved/_buttonOk").Invoke();

            Thread.Sleep(250);

            // Name = 'File Download'
            // Type = 'ControlType.Window'

            // Name =  'Browse For Folder'
            // Type =  'ControlType.Window'
                // Name =  'OK'
                // Type =  'ControlType.Button'

            // Type = 'ControlType.Window'
            // Id = 'FormFileSaved'

        }
        
        private AutomationElement GetSaveButton() 
        {
            return Element.FindByIdPath("prmPanelButtons/btnSaveClinicalData");            
        }       
    }
}