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
using Klepach.Core.VHDV.Client.Properties;
using Klepach.Core.VHDV.Db;
using Microsoft.EntityFrameworkCore;
using static Klepach.Core.VHDV.Db.AppDbContext;

namespace Klepach.Core.VHDV.Client
{
    public partial class Main : Form
    {
        #region variable
        AppDbContext _db;
        string _databasePath = "";
        VhdvDbType _dbType;
        int _currentPartitionId = -1;
        string viewType = "list";
        List<string> logList;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Main"/> class.
        /// </summary>
        public Main()
        {
            InitializeComponent();
        }
        #endregion

        #region Form
        /// <summary>
        /// Handles the Load event of the Main control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Main_Load(object sender, EventArgs e)
        {
            CommonFunctions.SettingWindowSizeLoad(this);
            InitListView();
            EnDisable(false);

            // open the last database
            _databasePath = Settings.Default.DatabasePath;
            if (!string.IsNullOrEmpty(_databasePath))
            {
                if (Enum.IsDefined(typeof(VhdvDbType), Settings.Default.DatabaseType))
                    _dbType = (VhdvDbType)Enum.Parse(typeof(VhdvDbType), Settings.Default.DatabaseType);
                OpenDatabaseConnection();
            }

        }

        /// <summary>
        /// Handles the FormClosed event of the Main control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosedEventArgs"/> instance containing the event data.</param>
        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            CommonFunctions.SettingWindowSizeSave(this, true);
        }
        #endregion

        #region Functions
        /// <summary>
        /// Creates new sqlitedatabase.
        /// </summary>
        private void NewSQLiteDatabase()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = Path.GetDirectoryName(_databasePath),
                FileName = "vhdvDatabase.db",
                Title = "New SQLite Database file",

                CheckFileExists = false,
                CheckPathExists = true,

                DefaultExt = "db",
                Filter = "SQLite files (*.db)|*.db",
                FilterIndex = 2,
                RestoreDirectory = true,
            };

            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            _databasePath = openFileDialog.FileName;
            // check if file exists
            if (File.Exists( _databasePath))
            {
                var dialogReturn = MessageBox.Show("Database exists - override?", "New Database", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2 );
                if (dialogReturn == DialogResult.No)
                    return;
            }

            _db = new AppDbContext(_databasePath, _dbType);

            _db.Database.EnsureCreated();

            Settings.Default.DatabasePath = _databasePath;
            Settings.Default.DatabaseType = _dbType.ToString();
            Settings.Default.Save();

            OpenDatabaseConnection();
        }
        /// <summary>
        /// Opens the sq lite database.
        /// </summary>
        private void OpenSQLiteDatabase()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = _databasePath,
                FileName = _databasePath,
                Title = "Open SQLite Database file",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "db",
                Filter = "SQLite files (*.db)|*.db",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;
            _databasePath = openFileDialog.FileName;

            Settings.Default.DatabasePath = _databasePath;
            Settings.Default.DatabaseType = _dbType.ToString();
            Settings.Default.Save();

            OpenDatabaseConnection();
        }

        /// <summary>
        /// Opens the database.
        /// </summary>
        private void OpenDatabaseConnection()
        {
            _db = new AppDbContext(_databasePath, _dbType);
            _db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            this.Text = $"Volume Offline Viewer - {_databasePath}";
            EnDisable(true);
            LoadPartitionsList();
        }

        /// <summary>
        /// Ens the disable.
        /// </summary>
        /// <param name="enable">if set to <c>true</c> [enable].</param>
        private void EnDisable(bool enable)
        {
            tsbInfo.Enabled = enable;
            cmbPartitions.Enabled = enable;
            tvFolder.Enabled = enable;
            lvFolderAndFiles.Enabled = enable;
            txtSuche.Enabled = enable;
            btnSuche.Enabled = enable;
        }

        /// <summary>
        /// Loads the partitions list.
        /// </summary>
        private void LoadPartitionsList()
        {
            cmbPartitions.Items.Clear();
            List<VHDVPartition> partitions = _db.Partitions.AsNoTracking().ToList();
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
            List<VHDVFileSystemItem> dirRecords = _db.FileSystemItems.AsNoTracking()
                .Where(r => r.PartitionId == _currentPartitionId && r.Type == "dir" && r.Path == path)
                .OrderBy(r => r.Name.ToLower())
                .ToList();
            foreach (var dirRecord in dirRecords)
            {
                TreeNode treeNode = new TreeNode();
                treeNode.Text = dirRecord.Name;
                treeNode.Tag = $"{(dirRecord.Path != "\\" ? dirRecord.Path : "")}\\{dirRecord.Name}-[{dirRecord.PartitionId}]";

                treeNode.ImageKey = "folder-open";
                treeNode.SelectedImageKey = "folder-open";
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
        private void LoadCurrentLevel(TreeNode selectedNode, string searchText)
        {
            viewType = "list";
            if (searchText != null) viewType = "search";


            var path = "\\";
            if (viewType == "list" && selectedNode != null) path = selectedNode.Name;
            if (viewType == "search") searchText = searchText.ToLowerInvariant();

            // get all the directories from this Level
            List<VHDVFileSystemItem> dirRecords;

            if (viewType == "list")
            {
                // list items from selected node
                InitListView();
                dirRecords = _db.FileSystemItems.AsNoTracking()
                    .Where(r => r.PartitionId == _currentPartitionId && r.Path == path)
                    .OrderBy(r => r.Type).ThenBy(r => r.Name.ToLower())
                    .ToList();
            }
            else
            {
                // search for text
                InitListView();
                dirRecords = _db.FileSystemItems.AsNoTracking()
                    //.Join(db.Partitions, r => r.PartitionId, p => p.PartitionId (r, p) )
                    .Where(r => r.Name.ToLower().Contains(searchText) || r.Path.ToLower().Contains(searchText))
                    .OrderBy(r => r.Name.ToLower())
                    .ToList();
            }

            statusStrip.Items[0].Text = $"{dirRecords.Count} items";
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
                    item.ImageKey = "folder-open";
                }
                else
                {
                    item.ImageKey = "document";
                    if (!string.IsNullOrEmpty(Path.GetExtension(dirRecord.Name)))
                        itemType = Path.GetExtension(dirRecord.Name).Substring(1);
                }
                item.Tag = $"{(dirRecord.Path != "\\" ? dirRecord.Path : "")}\\{dirRecord.Name}-[{dirRecord.PartitionId}]";
                if (viewType == "list")
                {
                    subItems = new ListViewItem.ListViewSubItem[]
                    {
                        new ListViewItem.ListViewSubItem(item, dirRecord.LastModifiered.ToShortDateString()),
                        new ListViewItem.ListViewSubItem(item, itemType),
                        new ListViewItem.ListViewSubItem(item, dirRecord.Type == "dir" ? "" : itemSize)
                    };
                    subItems[0].Name = "LastModifiered";
                    subItems[1].Name = "Type";
                    subItems[2].Name = "Size";
                }
                else
                {
                    subItems = new ListViewItem.ListViewSubItem[]
                    {
                        new ListViewItem.ListViewSubItem(item, dirRecord.Path),
                        new ListViewItem.ListViewSubItem(item, dirRecord.LastModifiered.ToShortDateString()),
                        new ListViewItem.ListViewSubItem(item, itemType),
                        new ListViewItem.ListViewSubItem(item, dirRecord.Type == "dir" ? "" : itemSize)
                    };
                    subItems[0].Name = "Path";
                    subItems[1].Name = "LastModifiered";
                    subItems[2].Name = "Type";
                    subItems[3].Name = "Size";
                }
                item.SubItems.AddRange(subItems);
                lvFolderAndFiles.Items.Add(item);
            }
            lvFolderAndFiles.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }
        /// <summary>
        /// Does the search.
        /// </summary>
        private void DoSearch()
        {
            if (txtSuche.Text == "")
                return;

            LoadCurrentLevel(null, txtSuche.Text);       
        }
        /// <summary>
        /// Initializes the ListView.
        /// </summary>
        /// <param name="type">The type.</param>
        private void InitListView()
        {
            lvFolderAndFiles.Clear();
            if (viewType == "list")
            {
                // Add columns
                lvFolderAndFiles.Columns.Add("Name", "Name");
                lvFolderAndFiles.Columns.Add("LastModifiered", "LastModifiered");
                lvFolderAndFiles.Columns.Add("Type", "Type");
                lvFolderAndFiles.Columns.Add("Size", -1, HorizontalAlignment.Right);
                lvFolderAndFiles.Columns.Add("", -1);
            }
            else
            {
                // Add columns
                lvFolderAndFiles.Columns.Add("Name", "Name");
                lvFolderAndFiles.Columns.Add("Path", "Path");
                lvFolderAndFiles.Columns.Add("LastModifiered", "LastModifiered");
                lvFolderAndFiles.Columns.Add("Type", "Type");
                lvFolderAndFiles.Columns.Add("Size", -1, HorizontalAlignment.Right);
                lvFolderAndFiles.Columns.Add("", -1);
            }
            lvFolderAndFiles.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
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
            tvFolder.Nodes.Clear();
            var partition = ((VHDVPartition)cmbPartitions.SelectedItem);
            _currentPartitionId = partition.Id;
            LoadLevel(null);
            if (viewType == "search")
                return;

            lvFolderAndFiles.Items.Clear();
            LoadCurrentLevel(null, null);
        }

        /// <summary>
        /// Handles the AfterSelect event of the tvFolder control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TreeViewEventArgs"/> instance containing the event data.</param>
        private void tvFolder_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var selectedNode = e.Node;
            LoadCurrentLevel(selectedNode, null);
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

        /// <summary>
        /// Handles the MouseDoubleClick event of the lvFolderAndFiles control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void lvFolderAndFiles_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var senderList = (ListView)sender;
            var clickedItem = senderList.HitTest(e.Location).Item;
            if (clickedItem == null)
                return;

            var type = clickedItem.SubItems["Type"].Text;
            var openFolderPath = clickedItem.Tag.ToString();

            if (viewType == "search")
            {
                var partitionId = clickedItem.Tag.ToString();
                partitionId = partitionId.Substring(partitionId.LastIndexOf("-[") + 2);
                partitionId = partitionId.Substring(0, partitionId.Length - 1);

                foreach (VHDVPartition cmbItem in cmbPartitions.Items)
                {
                    if (cmbItem.Id == int.Parse(partitionId))
                    {
                        cmbPartitions.SelectedItem = cmbItem;
                        break;
                    }
                }

                if (type != "dir") openFolderPath = Path.GetDirectoryName(openFolderPath) + "-[" + partitionId.ToString() +"]";
            }
            else
            {
                if (type != "dir") return;
            }

            openFolderPath = openFolderPath.Substring(0, openFolderPath.IndexOf("-["));

            // find the Node in the Tree an expand
            TreeNode foundNode = null;
            TreeNodeCollection treeNodes = tvFolder.Nodes;
            var folderPaths = openFolderPath.Split("\\");
            string pathToFind = "";
            TreeNode[] foundNodes;
            
            for (var xi = 0; xi < folderPaths.Length; xi++)
            {
                logList.Add(folderPaths[xi]);
                if (string.IsNullOrEmpty(folderPaths[xi])) continue;
                pathToFind += "\\" + folderPaths[xi];
                foundNodes = treeNodes.Find(pathToFind, false);
                if (foundNodes.Length == 0)
                    break;
                foundNode = foundNodes[0];
                if (foundNode.Nodes.Count == 0)
                {
                    LoadLevel(foundNode);
                }
                foundNode.Expand();
                treeNodes = foundNode.Nodes;
            }

            if (viewType == "search")
                return;

            foreach (TreeNode subTreeNode in foundNode.Nodes)
                LoadLevel(subTreeNode);
            LoadCurrentLevel(foundNode, null);
        }
        /// <summary>
        /// Gets the node.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="rootNode">The root node.</param>
        /// <returns></returns>
        public TreeNode GetNode(string tag, TreeNode rootNode)
        {
            TreeNodeCollection nodes;
            if (rootNode == null)
                nodes = tvFolder.Nodes;
            else
                nodes = rootNode.Nodes;
            foreach (TreeNode node in nodes)
            {
                logList.Add(node.Tag.ToString());

                if (node.Tag.ToString().Equals(tag)) return node;

                //recursion
                var next = GetNode(tag, node);
                if (next != null) return next;
            }
            return null;
        }
        /// <summary>
        /// Handles the KeyPress event of the txtSuche control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void txtSuche_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Enter)
                return;

            DoSearch();
        }
        /// <summary>
        /// Handles the Click event of the btnSuche control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnSuche_Click(object sender, EventArgs e)
        {
            DoSearch();
        }
        /// <summary>
        /// Handles the Click event of the TsbOpenDatabase control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <exception cref="NotImplementedException"></exception>
        private void tsbNewDatabase_Click(object sender, EventArgs e)
        {
            NewSQLiteDatabase();
        }
        /// <summary>
        /// Handles the Click event of the tsbOpenDatabase control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void tsbOpenDatabase_Click(object sender, EventArgs e)
        {
            OpenSQLiteDatabase();
        }
        /// <summary>
        /// Handles the Click event of the tsbScanPartition control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void tsbScanPartition_Click(object sender, EventArgs e)
        {
            try
            {
                FrmScanPartition frmScanPartition = new FrmScanPartition();
                frmScanPartition.db = _db;
                frmScanPartition.ShowDialog();
                LoadPartitionsList();
                tvFolder.Nodes.Clear();
                lvFolderAndFiles.Items.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error scanning drive:\n{ex.Message}", "Scanning drive", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

    }
}
