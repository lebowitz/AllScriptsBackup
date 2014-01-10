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
            var dataDir = new DirectoryInfo(txtDataLocation.Text);
            Ripper.Start(_cts, dataDir, txtIdPrefix.Text, txtIdFrom.Text, txtIdTo.Text);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _cts.Cancel();
        }
    }
}