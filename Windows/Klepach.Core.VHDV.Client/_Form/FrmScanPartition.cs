using Klepach.Core.VHDV.Db;
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

namespace Klepach.Core.VHDV.Client
{
    public partial class FrmScanPartition : Form
    {
        #region variable
        /// <summary>
        /// The database
        /// </summary>
        public AppDbContext db;
        Inventory _inventory = null;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="FrmScanPartition"/> class.
        /// </summary>
        public FrmScanPartition()
        {
            InitializeComponent();
        }
        #endregion

        #region Form
        /// <summary>
        /// Handles the Load event of the FrmScanPartition control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void FrmScanPartition_Load(object sender, EventArgs e)
        {
            CommonFunctions.SettingWindowSizeLoad(this);
            EnDisable(true);

            bool isSelected = false;
            string volumeLabel;
            int selectedIndex;
            foreach (var drive in DriveInfo.GetDrives())
            {
                // only local drives
                if (drive.DriveType != DriveType.Network && drive.DriveType != DriveType.Ram)
                {
                    try
                    {
                        volumeLabel = drive.VolumeLabel;
                    }
                    catch (Exception ex)
                    {
                        volumeLabel = ex.Message;
                    }

                    selectedIndex = cmbPartitions.Items.Add($"{drive.Name} ({volumeLabel}) - {drive.DriveType}");
                    if (!isSelected && drive.DriveType == DriveType.Fixed)
                    {
                        isSelected = true;
                        cmbPartitions.SelectedIndex = selectedIndex;
                    }
                }
            }

        }
        /// <summary>
        /// Handles the FormClosed event of the FrmScanPartition control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosedEventArgs"/> instance containing the event data.</param>
        private void FrmScanPartition_FormClosed(object sender, FormClosedEventArgs e)
        {
            CommonFunctions.SettingWindowSizeSave(this, true);
        }
        #endregion

        #region Functions
        /// <summary>
        /// Ens the disable.
        /// </summary>
        /// <param name="enable">if set to <c>true</c> [enable].</param>
        private void EnDisable(bool enable)
        {
            cmbPartitions.Enabled = enable;
            tsbStartScan.Visible = enable;
            tsbStopScan.Visible = !enable;
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
        }
        /// <summary>
        /// Handles the Click event of the tsbStartScan control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void tsbStartScan_Click(object sender, EventArgs e)
        {
            if (cmbPartitions.SelectedItem == null)
                return;
            EnDisable(false);

            var scanDrive = cmbPartitions.SelectedItem.ToString();
            scanDrive = scanDrive.Substring(0, scanDrive.IndexOf("(", StringComparison.OrdinalIgnoreCase));
            _inventory = new Inventory(db);
            _inventory.ScanStatus += Inventory_ScanStatus;
            _inventory.ScanDrive(scanDrive);
            _inventory = null;
            
            EnDisable(true);
        }
        /// <summary>
        /// Handles the Click event of the tsbStopScan control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void tsbStopScan_Click(object sender, EventArgs e)
        {
            _inventory.StopScan();
            EnDisable(false);
        }

        /// <summary>
        /// Handles the ScanStatus event of the Inventory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ScanStatusEventArgs"/> instance containing the event data.</param>
        private void Inventory_ScanStatus(object sender, ScanStatusEventArgs e)
        {
            statusStrip.Items[0].Text = e.Status + " ...";
            Application.DoEvents();
        }
        #endregion


    }
}
