using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

            iconImageList.Images.Add("dir", new Icon(SystemIcons.Exclamation, 40, 40));
            iconImageList.Images.Add("file", new Icon(SystemIcons.Error, 40, 40));
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
        private void LoadNextLevel(TreeNode selectedNode)
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

            foreach (var dirRecord in dirRecords)
            {
                ListViewItem.ListViewSubItem[] subItems;
                ListViewItem item = null;

                item = new ListViewItem(dirRecord.Name, 0);
                subItems = new ListViewItem.ListViewSubItem[]
                    {
                        new ListViewItem.ListViewSubItem(item, dirRecord.Type),
                        new ListViewItem.ListViewSubItem(item, dirRecord.Size.ToString()),
                        new ListViewItem.ListViewSubItem(item, dirRecord.LastModifiered.ToShortDateString())
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
            LoadNextLevel(null);
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
            if (selectedNode.Nodes.Count == 0)
                LoadNextLevel(selectedNode);
        }
    }
}
