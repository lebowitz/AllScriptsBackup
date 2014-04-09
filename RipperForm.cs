using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace AllScriptRipper
{
    public partial class RipperForm : Form
    {
        public RipperForm()
        {
            InitializeComponent();
            var listener = new TextBoxTraceListener(this.txtLog);
            Trace.Listeners.Add(listener);
        }

        private CancellationTokenSource _cts = new CancellationTokenSource();

        private void button1_Click(object sender, EventArgs e)
        {
            var options = new LoadOptions
            {
                ReleaseOfInformation = cbReleaseOfInformation.Checked,
                Demographics = cbDemographics.Checked,
                Index = txtIndex.Text,
                Type = txtType.Text,
                PatientIds = txtPatientIds.Lines,
                ElasticSearchUri = new Uri(txtElasticSearchUrl.Text)
            };

            Ripper.Start(_cts, options);
        }

        private DirectoryInfo DataDir
        {
            get
            {
                var dataDir = new DirectoryInfo(txtElasticSearchUrl.Text);
                return dataDir;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _cts.Cancel();
        }
    }
}
