﻿
namespace Klepach.Core.VHDV.Client
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbInfo = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cmbPartitions = new System.Windows.Forms.ComboBox();
            this.tvFolder = new System.Windows.Forms.TreeView();
            this.iconSmallImageList = new System.Windows.Forms.ImageList(this.components);
            this.btnSuche = new System.Windows.Forms.Button();
            this.txtSuche = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lvFolderAndFiles = new System.Windows.Forms.ListView();
            this.iconLargeImageList = new System.Windows.Forms.ImageList(this.components);
            this.tssl1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssl1});
            this.statusStrip.Location = new System.Drawing.Point(0, 429);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(805, 22);
            this.statusStrip.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbInfo});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(805, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbInfo
            // 
            this.tsbInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbInfo.Image = ((System.Drawing.Image)(resources.GetObject("tsbInfo.Image")));
            this.tsbInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbInfo.Name = "tsbInfo";
            this.tsbInfo.Size = new System.Drawing.Size(23, 22);
            this.tsbInfo.Text = "&New";
            this.tsbInfo.ToolTipText = "Partition Info";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.cmbPartitions);
            this.splitContainer1.Panel1.Controls.Add(this.tvFolder);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuBar;
            this.splitContainer1.Panel2.Controls.Add(this.btnSuche);
            this.splitContainer1.Panel2.Controls.Add(this.txtSuche);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.lvFolderAndFiles);
            this.splitContainer1.Size = new System.Drawing.Size(805, 404);
            this.splitContainer1.SplitterDistance = 267;
            this.splitContainer1.TabIndex = 2;
            // 
            // cmbPartitions
            // 
            this.cmbPartitions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPartitions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPartitions.FormattingEnabled = true;
            this.cmbPartitions.Location = new System.Drawing.Point(4, 4);
            this.cmbPartitions.Name = "cmbPartitions";
            this.cmbPartitions.Size = new System.Drawing.Size(259, 23);
            this.cmbPartitions.TabIndex = 1;
            this.cmbPartitions.SelectedIndexChanged += new System.EventHandler(this.cmbPartitions_SelectedIndexChanged);
            // 
            // tvFolder
            // 
            this.tvFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvFolder.ImageIndex = 0;
            this.tvFolder.ImageList = this.iconSmallImageList;
            this.tvFolder.Location = new System.Drawing.Point(3, 33);
            this.tvFolder.Name = "tvFolder";
            this.tvFolder.SelectedImageIndex = 0;
            this.tvFolder.Size = new System.Drawing.Size(260, 367);
            this.tvFolder.TabIndex = 0;
            this.tvFolder.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvFolder_BeforeExpand);
            this.tvFolder.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvFolder_AfterSelect);
            // 
            // iconSmallImageList
            // 
            this.iconSmallImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.iconSmallImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iconSmallImageList.ImageStream")));
            this.iconSmallImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.iconSmallImageList.Images.SetKeyName(0, "document");
            this.iconSmallImageList.Images.SetKeyName(1, "drive-harddisk");
            this.iconSmallImageList.Images.SetKeyName(2, "drive-removable-media");
            this.iconSmallImageList.Images.SetKeyName(3, "folder");
            this.iconSmallImageList.Images.SetKeyName(4, "Info");
            // 
            // btnSuche
            // 
            this.btnSuche.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSuche.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSuche.BackgroundImage")));
            this.btnSuche.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSuche.Location = new System.Drawing.Point(509, 4);
            this.btnSuche.Name = "btnSuche";
            this.btnSuche.Size = new System.Drawing.Size(23, 23);
            this.btnSuche.TabIndex = 3;
            this.btnSuche.UseVisualStyleBackColor = true;
            this.btnSuche.Click += new System.EventHandler(this.btnSuche_Click);
            // 
            // txtSuche
            // 
            this.txtSuche.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSuche.Location = new System.Drawing.Point(48, 4);
            this.txtSuche.Name = "txtSuche";
            this.txtSuche.Size = new System.Drawing.Size(458, 23);
            this.txtSuche.TabIndex = 2;
            this.txtSuche.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSuche_KeyPress);
            // 
            // label1
            // 
            this.label1.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuBar;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Suche";
            // 
            // lvFolderAndFiles
            // 
            this.lvFolderAndFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvFolderAndFiles.HideSelection = false;
            this.lvFolderAndFiles.LargeImageList = this.iconLargeImageList;
            this.lvFolderAndFiles.Location = new System.Drawing.Point(3, 33);
            this.lvFolderAndFiles.Name = "lvFolderAndFiles";
            this.lvFolderAndFiles.Size = new System.Drawing.Size(527, 367);
            this.lvFolderAndFiles.SmallImageList = this.iconSmallImageList;
            this.lvFolderAndFiles.TabIndex = 0;
            this.lvFolderAndFiles.UseCompatibleStateImageBehavior = false;
            this.lvFolderAndFiles.View = System.Windows.Forms.View.Details;
            this.lvFolderAndFiles.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvFolderAndFiles_MouseDoubleClick);
            // 
            // iconLargeImageList
            // 
            this.iconLargeImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.iconLargeImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iconLargeImageList.ImageStream")));
            this.iconLargeImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.iconLargeImageList.Images.SetKeyName(0, "folder");
            this.iconLargeImageList.Images.SetKeyName(1, "document");
            // 
            // tssl1
            // 
            this.tssl1.Name = "tssl1";
            this.tssl1.Size = new System.Drawing.Size(0, 17);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(805, 451);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.Text = "Volume Offline Viewer";
            this.Load += new System.EventHandler(this.Main_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView tvFolder;
        private System.Windows.Forms.ListView lvFolderAndFiles;
        private System.Windows.Forms.ComboBox cmbPartitions;
        private System.Windows.Forms.ImageList iconLargeImageList;
        private System.Windows.Forms.ImageList iconSmallImageList;
        private System.Windows.Forms.ToolStripButton tsbInfo;
        private System.Windows.Forms.TextBox txtSuche;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSuche;
        private System.Windows.Forms.ToolStripStatusLabel tssl1;
    }
}

