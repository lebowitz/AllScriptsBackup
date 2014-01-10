namespace AllScriptRipper
{
    partial class RipperForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnGo = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.lblDataLocation = new System.Windows.Forms.Label();
            this.txtDataLocation = new System.Windows.Forms.TextBox();
            this.txtIdFrom = new System.Windows.Forms.TextBox();
            this.lblIdRange = new System.Windows.Forms.Label();
            this.lblTo = new System.Windows.Forms.Label();
            this.txtIdTo = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblIdPrefix = new System.Windows.Forms.Label();
            this.txtIdPrefix = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnGo
            // 
            this.btnGo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGo.Location = new System.Drawing.Point(259, 361);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(43, 23);
            this.btnGo.TabIndex = 0;
            this.btnGo.Text = "Go";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.Location = new System.Drawing.Point(12, 82);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(361, 268);
            this.txtLog.TabIndex = 1;
            // 
            // lblDataLocation
            // 
            this.lblDataLocation.AutoSize = true;
            this.lblDataLocation.Location = new System.Drawing.Point(9, 12);
            this.lblDataLocation.Name = "lblDataLocation";
            this.lblDataLocation.Size = new System.Drawing.Size(108, 13);
            this.lblDataLocation.TabIndex = 2;
            this.lblDataLocation.Text = "Target Data Location";
            // 
            // txtDataLocation
            // 
            this.txtDataLocation.Location = new System.Drawing.Point(123, 9);
            this.txtDataLocation.Name = "txtDataLocation";
            this.txtDataLocation.Size = new System.Drawing.Size(250, 20);
            this.txtDataLocation.TabIndex = 3;
            this.txtDataLocation.Text = "c:\\belliacres-data";
            // 
            // txtIdFrom
            // 
            this.txtIdFrom.Location = new System.Drawing.Point(123, 34);
            this.txtIdFrom.Name = "txtIdFrom";
            this.txtIdFrom.Size = new System.Drawing.Size(109, 20);
            this.txtIdFrom.TabIndex = 5;
            this.txtIdFrom.Text = "8026";
            // 
            // lblIdRange
            // 
            this.lblIdRange.AutoSize = true;
            this.lblIdRange.Location = new System.Drawing.Point(9, 37);
            this.lblIdRange.Name = "lblIdRange";
            this.lblIdRange.Size = new System.Drawing.Size(51, 13);
            this.lblIdRange.TabIndex = 4;
            this.lblIdRange.Text = "Id Range";
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Location = new System.Drawing.Point(238, 37);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(16, 13);
            this.lblTo.TabIndex = 6;
            this.lblTo.Text = "to";
            // 
            // txtIdTo
            // 
            this.txtIdTo.Location = new System.Drawing.Point(260, 34);
            this.txtIdTo.Name = "txtIdTo";
            this.txtIdTo.Size = new System.Drawing.Size(113, 20);
            this.txtIdTo.TabIndex = 7;
            this.txtIdTo.Text = "10000";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(308, 361);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(60, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblIdPrefix
            // 
            this.lblIdPrefix.AutoSize = true;
            this.lblIdPrefix.Location = new System.Drawing.Point(9, 59);
            this.lblIdPrefix.Name = "lblIdPrefix";
            this.lblIdPrefix.Size = new System.Drawing.Size(69, 13);
            this.lblIdPrefix.TabIndex = 9;
            this.lblIdPrefix.Text = "Id Prefix (opt)";
            // 
            // txtIdPrefix
            // 
            this.txtIdPrefix.Location = new System.Drawing.Point(123, 56);
            this.txtIdPrefix.Name = "txtIdPrefix";
            this.txtIdPrefix.Size = new System.Drawing.Size(250, 20);
            this.txtIdPrefix.TabIndex = 10;
            this.txtIdPrefix.Text = "Imp";
            // 
            // RipperForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 391);
            this.Controls.Add(this.txtIdPrefix);
            this.Controls.Add(this.lblIdPrefix);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtIdTo);
            this.Controls.Add(this.lblTo);
            this.Controls.Add(this.txtIdFrom);
            this.Controls.Add(this.lblIdRange);
            this.Controls.Add(this.txtDataLocation);
            this.Controls.Add(this.lblDataLocation);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.btnGo);
            this.Name = "RipperForm";
            this.Text = "AllScripts Ripper";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Label lblDataLocation;
        private System.Windows.Forms.TextBox txtDataLocation;
        private System.Windows.Forms.TextBox txtIdFrom;
        private System.Windows.Forms.Label lblIdRange;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.TextBox txtIdTo;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblIdPrefix;
        private System.Windows.Forms.TextBox txtIdPrefix;
    }
}