
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
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.tsslText = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsbStopScan = new System.Windows.Forms.ToolStripButton();
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
            this.cmbPartitions.Location = new System.Drawing.Point(83, 72);
            this.cmbPartitions.Name = "cmbPartitions";
            this.cmbPartitions.Size = new System.Drawing.Size(386, 23);
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
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslText});
            this.statusStrip.Location = new System.Drawing.Point(0, 225);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(517, 22);
            this.statusStrip.TabIndex = 3;
            // 
            // tsslText
            // 
            this.tsslText.Name = "tsslText";
            this.tsslText.Size = new System.Drawing.Size(0, 17);
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
            // FrmScanPartition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 247);
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
    }
}