
namespace Klepach.Core.VHDV.Client
{
    partial class FrmScanPartition
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmScanPartition));
            this.label1 = new System.Windows.Forms.Label();
            this.cmbPartitions = new System.Windows.Forms.ComboBox();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.tsbStartScan = new System.Windows.Forms.ToolStripButton();
            this.tsbStopScan = new System.Windows.Forms.ToolStripButton();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.tsslText = new System.Windows.Forms.ToolStripStatusLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.lblSerialNo = new System.Windows.Forms.Label();
            this.lblVolumeName = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblFileSystem = new System.Windows.Forms.Label();
            this.lblFoundInDb = new System.Windows.Forms.Label();
            this.toolStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Partition:";
            // 
            // cmbPartitions
            // 
            this.cmbPartitions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPartitions.FormattingEnabled = true;
            this.cmbPartitions.Location = new System.Drawing.Point(100, 72);
            this.cmbPartitions.Name = "cmbPartitions";
            this.cmbPartitions.Size = new System.Drawing.Size(369, 23);
            this.cmbPartitions.TabIndex = 1;
            this.cmbPartitions.SelectedIndexChanged += new System.EventHandler(this.cmbPartitions_SelectedIndexChanged);
            // 
            // toolStrip
            // 
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbStartScan,
            this.tsbStopScan});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(517, 55);
            this.toolStrip.TabIndex = 2;
            // 
            // tsbStartScan
            // 
            this.tsbStartScan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbStartScan.Image = ((System.Drawing.Image)(resources.GetObject("tsbStartScan.Image")));
            this.tsbStartScan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbStartScan.Name = "tsbStartScan";
            this.tsbStartScan.Size = new System.Drawing.Size(52, 52);
            this.tsbStartScan.Text = "Start scan";
            this.tsbStartScan.Click += new System.EventHandler(this.tsbStartScan_Click);
            // 
            // tsbStopScan
            // 
            this.tsbStopScan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbStopScan.Image = ((System.Drawing.Image)(resources.GetObject("tsbStopScan.Image")));
            this.tsbStopScan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbStopScan.Name = "tsbStopScan";
            this.tsbStopScan.Size = new System.Drawing.Size(52, 52);
            this.tsbStopScan.Text = "Stop scan";
            this.tsbStopScan.Visible = false;
            this.tsbStopScan.Click += new System.EventHandler(this.tsbStopScan_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslText});
            this.statusStrip.Location = new System.Drawing.Point(0, 224);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(517, 22);
            this.statusStrip.TabIndex = 3;
            // 
            // tsslText
            // 
            this.tsslText.Name = "tsslText";
            this.tsslText.Size = new System.Drawing.Size(0, 17);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Serial-No:";
            // 
            // lblSerialNo
            // 
            this.lblSerialNo.AutoSize = true;
            this.lblSerialNo.Location = new System.Drawing.Point(100, 116);
            this.lblSerialNo.Name = "lblSerialNo";
            this.lblSerialNo.Size = new System.Drawing.Size(51, 15);
            this.lblSerialNo.TabIndex = 5;
            this.lblSerialNo.Text = "SerialNo";
            // 
            // lblVolumeName
            // 
            this.lblVolumeName.AutoSize = true;
            this.lblVolumeName.Location = new System.Drawing.Point(100, 140);
            this.lblVolumeName.Name = "lblVolumeName";
            this.lblVolumeName.Size = new System.Drawing.Size(39, 15);
            this.lblVolumeName.TabIndex = 7;
            this.lblVolumeName.Text = "Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 140);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "Name:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(-89, 211);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 15);
            this.label5.TabIndex = 9;
            this.label5.Text = "SerialNo";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(-150, 211);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 15);
            this.label6.TabIndex = 8;
            this.label6.Text = "File System:";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(100, 167);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(67, 15);
            this.lblDescription.TabIndex = 11;
            this.lblDescription.Text = "Description";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(22, 167);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 15);
            this.label7.TabIndex = 10;
            this.label7.Text = "Description:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 192);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 15);
            this.label3.TabIndex = 12;
            this.label3.Text = "File System:";
            // 
            // lblFileSystem
            // 
            this.lblFileSystem.AutoSize = true;
            this.lblFileSystem.Location = new System.Drawing.Point(100, 192);
            this.lblFileSystem.Name = "lblFileSystem";
            this.lblFileSystem.Size = new System.Drawing.Size(63, 15);
            this.lblFileSystem.TabIndex = 13;
            this.lblFileSystem.Text = "FileSystem";
            // 
            // lblFoundInDb
            // 
            this.lblFoundInDb.Location = new System.Drawing.Point(294, 116);
            this.lblFoundInDb.Name = "lblFoundInDb";
            this.lblFoundInDb.Size = new System.Drawing.Size(175, 39);
            this.lblFoundInDb.TabIndex = 14;
            this.lblFoundInDb.Text = "Found in DB";
            // 
            // FrmScanPartition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 246);
            this.Controls.Add(this.lblFoundInDb);
            this.Controls.Add(this.lblFileSystem);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblVolumeName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblSerialNo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.cmbPartitions);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmScanPartition";
            this.Text = "Scan Partition";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmScanPartition_FormClosed);
            this.Load += new System.EventHandler(this.FrmScanPartition_Load);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbPartitions;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton tsbStartScan;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel tsslText;
        private System.Windows.Forms.ToolStripButton tsbStopScan;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblSerialNo;
        private System.Windows.Forms.Label lblVolumeName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblFileSystem;
        private System.Windows.Forms.Label lblFoundInDb;
    }
}