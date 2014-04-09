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
            this.lblElasticSearchUrl = new System.Windows.Forms.Label();
            this.txtElasticSearchUrl = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtIndex = new System.Windows.Forms.TextBox();
            this.lblIndex = new System.Windows.Forms.Label();
            this.txtType = new System.Windows.Forms.TextBox();
            this.lblType = new System.Windows.Forms.Label();
            this.txtPatientIds = new System.Windows.Forms.TextBox();
            this.cbDemographics = new System.Windows.Forms.CheckBox();
            this.cbReleaseOfInformation = new System.Windows.Forms.CheckBox();
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
            this.txtLog.Location = new System.Drawing.Point(12, 247);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(361, 108);
            this.txtLog.TabIndex = 1;
            // 
            // lblElasticSearchUrl
            // 
            this.lblElasticSearchUrl.AutoSize = true;
            this.lblElasticSearchUrl.Location = new System.Drawing.Point(9, 12);
            this.lblElasticSearchUrl.Name = "lblElasticSearchUrl";
            this.lblElasticSearchUrl.Size = new System.Drawing.Size(88, 13);
            this.lblElasticSearchUrl.TabIndex = 2;
            this.lblElasticSearchUrl.Text = "ElasticSearch Url";
            // 
            // txtElasticSearchUrl
            // 
            this.txtElasticSearchUrl.Location = new System.Drawing.Point(123, 9);
            this.txtElasticSearchUrl.Name = "txtElasticSearchUrl";
            this.txtElasticSearchUrl.Size = new System.Drawing.Size(250, 20);
            this.txtElasticSearchUrl.TabIndex = 3;
            this.txtElasticSearchUrl.Text = "http://localhost:9200";
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
            // txtIndex
            // 
            this.txtIndex.Location = new System.Drawing.Point(123, 35);
            this.txtIndex.Name = "txtIndex";
            this.txtIndex.Size = new System.Drawing.Size(250, 20);
            this.txtIndex.TabIndex = 13;
            this.txtIndex.Text = "allscripts";
            // 
            // lblIndex
            // 
            this.lblIndex.AutoSize = true;
            this.lblIndex.Location = new System.Drawing.Point(9, 38);
            this.lblIndex.Name = "lblIndex";
            this.lblIndex.Size = new System.Drawing.Size(33, 13);
            this.lblIndex.TabIndex = 12;
            this.lblIndex.Text = "Index";
            // 
            // txtType
            // 
            this.txtType.Location = new System.Drawing.Point(123, 61);
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(250, 20);
            this.txtType.TabIndex = 15;
            this.txtType.Text = "patient";
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(9, 64);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(31, 13);
            this.lblType.TabIndex = 14;
            this.lblType.Text = "Type";
            // 
            // txtPatientIds
            // 
            this.txtPatientIds.Location = new System.Drawing.Point(12, 114);
            this.txtPatientIds.Multiline = true;
            this.txtPatientIds.Name = "txtPatientIds";
            this.txtPatientIds.Size = new System.Drawing.Size(354, 127);
            this.txtPatientIds.TabIndex = 16;
            this.txtPatientIds.Text = "1\r\n2\r\n3\r\n4\r\n5";
            // 
            // cbDemographics
            // 
            this.cbDemographics.AutoSize = true;
            this.cbDemographics.Location = new System.Drawing.Point(12, 91);
            this.cbDemographics.Name = "cbDemographics";
            this.cbDemographics.Size = new System.Drawing.Size(94, 17);
            this.cbDemographics.TabIndex = 18;
            this.cbDemographics.Text = "Demographics";
            this.cbDemographics.UseVisualStyleBackColor = true;
            // 
            // cbReleaseOfInformation
            // 
            this.cbReleaseOfInformation.AutoSize = true;
            this.cbReleaseOfInformation.Location = new System.Drawing.Point(112, 91);
            this.cbReleaseOfInformation.Name = "cbReleaseOfInformation";
            this.cbReleaseOfInformation.Size = new System.Drawing.Size(100, 17);
            this.cbReleaseOfInformation.TabIndex = 19;
            this.cbReleaseOfInformation.Text = "Release Of Info";
            this.cbReleaseOfInformation.UseVisualStyleBackColor = true;
            // 
            // RipperForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 391);
            this.Controls.Add(this.cbReleaseOfInformation);
            this.Controls.Add(this.cbDemographics);
            this.Controls.Add(this.txtPatientIds);
            this.Controls.Add(this.txtType);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.txtIndex);
            this.Controls.Add(this.lblIndex);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtElasticSearchUrl);
            this.Controls.Add(this.lblElasticSearchUrl);
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
        private System.Windows.Forms.Label lblElasticSearchUrl;
        private System.Windows.Forms.TextBox txtElasticSearchUrl;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtIndex;
        private System.Windows.Forms.Label lblIndex;
        private System.Windows.Forms.TextBox txtType;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.TextBox txtPatientIds;
        private System.Windows.Forms.CheckBox cbDemographics;
        private System.Windows.Forms.CheckBox cbReleaseOfInformation;
    }
}