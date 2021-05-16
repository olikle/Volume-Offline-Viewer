using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Klepach.Core.VHDV.Db;
using Microsoft.EntityFrameworkCore;

namespace Klepach.Core.VHDV.Client
{
    public partial class Main : Form
    {
        AppDbContext db;
        int _currentPartitionId = -1;
        /// <summary>
        /// Initializes a new instance of the <see cref="Main"/> class.
        /// </summary>
        public Main()
        {
            db = new AppDbContext();

            InitializeComponent();
        }

        #region Form
        /// <summary>
        /// Handles the Load event of the Main control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Main_Load(object sender, EventArgs e)
        {
            LoadPartitionsList();
        }
        #endregion

        #region Functions
        /// <summary>
        /// Loads the partitions list.
        /// </summary>
        private void LoadPartitionsList()
        {
            cmbPartitions.Items.Clear();
            List<VOVPartition> partitions = db.Partitions.AsNoTracking().ToList();
            foreach (var partition in partitions)
                cmbPartitions.Items.Add(partition);
        }
        /// <summary>
        /// Loads the next level.
        /// </summary>
        /// <param name="selectedNode">The selected node.</param>
        private void LoadLevel(TreeNode selectedNode)
        {
            var path = "\\";
            if (selectedNode != null)
                path = selectedNode.Name;

            // get all the directories from this Level
            List<VOVFileSystemItem> dirRecords = db.FileSystemItems.AsNoTracking()
                .Where(r => r.PartitionId == _currentPartitionId && r.Type == "dir" && r.Path == path)
                .OrderBy(r => r.Name.ToLower())
                .ToList();
            foreach (var dirRecord in dirRecords)
            {
                TreeNode treeNode = new TreeNode();
                treeNode.Text = dirRecord.Name;
                treeNode.Tag = dirRecord.Name;
                treeNode.ImageKey = "folder";
                treeNode.SelectedImageKey = "folder";
                if (selectedNode == null)
                {
                    treeNode.Name = dirRecord.Path + dirRecord.Name;
                    tvFolder.Nodes.Add(treeNode);
                }
                else
                {
                    treeNode.Name = dirRecord.Path + "\\" + dirRecord.Name;
                    selectedNode.Nodes.Add(treeNode);
                }
                if (path == "\\")
                    LoadLevel(treeNode);
            }
        }
        /// <summary>
        /// Loads the current level.
        /// </summary>
        /// <param name="selectedNode">The selected node.</param>
        private void LoadCurrentLevel(TreeNode selectedNode)
        {
            var path = "\\";
            if (selectedNode != null) path = selectedNode.Name;

            // get all the directories from this Level
            List<VOVFileSystemItem> dirRecords = db.FileSystemItems.AsNoTracking()
                .Where(r => r.PartitionId == _currentPartitionId && r.Path == path)
                .OrderBy(r => r.Type).ThenBy(r => r.Name.ToLower())
                .ToList();

            lvFolderAndFiles.Items.Clear();
            var itemType = "";
            foreach (var dirRecord in dirRecords)
            {
                ListViewItem.ListViewSubItem[] subItems;
                ListViewItem item = null;

                item = new ListViewItem(dirRecord.Name, 0);
                itemType = dirRecord.Type;
                var itemSize = dirRecord.Size.ToString() + " B";
                if (dirRecord.Size > 1024)
                    itemSize = (dirRecord.Size / 1024).ToString("###,###,###") + " KB";

                if (dirRecord.Type == "dir")
                {
                    item.ImageKey = "folder";
                }
                else
                {
                    item.ImageKey = "document";
                    itemType = Path.GetExtension(dirRecord.Name).Substring(1);
                }
                subItems = new ListViewItem.ListViewSubItem[]
                {
                    new ListViewItem.ListViewSubItem(item, dirRecord.LastModifiered.ToShortDateString()),
                    new ListViewItem.ListViewSubItem(item, itemType),
                    new ListViewItem.ListViewSubItem(item, dirRecord.Type == "dir" ? "" : itemSize)
                };
                item.SubItems.AddRange(subItems);
                lvFolderAndFiles.Items.Add(item);

                lvFolderAndFiles.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
        }
        #endregion

        #region UI
        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbPartitions control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cmbPartitions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPartitions.SelectedItem == null)
                return;
            var partition = ((VOVPartition)cmbPartitions.SelectedItem);
            _currentPartitionId = partition.Id;
            LoadLevel(null);
            LoadCurrentLevel(null);
        }
        #endregion

        /// <summary>
        /// Handles the AfterSelect event of the tvFolder control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TreeViewEventArgs"/> instance containing the event data.</param>
        private void tvFolder_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var selectedNode = e.Node;
            LoadCurrentLevel(selectedNode);
        }
        /// <summary>
        /// Handles the BeforeExpand event of the tvFolder control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TreeViewCancelEventArgs"/> instance containing the event data.</param>
        private void tvFolder_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            var selectedNode = e.Node;
            // load next Level
            foreach (TreeNode subTreeNode in selectedNode.Nodes)
                LoadLevel(subTreeNode);
        }
    }
}
