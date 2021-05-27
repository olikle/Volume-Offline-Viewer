using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Klepach.Core.VHDV.Db
{
    /// <summary>
    /// Inventory
    /// </summary>
    public class Inventory
    {
        #region variables
        AppDbContext _db;
        bool _stopScan = false;
        #endregion

        #region constructor
        /// <summary>Initializes a new instance of the <see cref="Inventory" /> class.</summary>
        /// <param name="db">The database.</param>
        public Inventory(AppDbContext db)
        {
            _db = db;
        }
        #endregion

        #region ScanDrive, SetDiskInfo, SetPartitionInfo, ScanFilesAndFolders
        /// <summary>
        /// Scans the drive.
        /// </summary>
        /// <param name="driveLetter">The drive letter.</param>
        public void ScanDrive(string driveLetter)
        {
            if (driveLetter.Contains(":")) driveLetter = driveLetter.Substring(0, driveLetter.IndexOf(":", StringComparison.OrdinalIgnoreCase)+1);
            
            _stopScan = false;

            var diskRecord = SetDiskInfo(driveLetter);
            var partitionRecord = SetPartitionInfo(driveLetter, diskRecord.Id);
            // scan the files
            ScanFilesAndFolders(driveLetter, partitionRecord.Id);
        }
        public void StopScan()
        {
            _stopScan = true;
        }
        /// <summary>
        /// Sets the disk information.
        /// </summary>
        /// <param name="driveLetter">The drive letter.</param>
        /// <returns></returns>
        private VHDVDisk SetDiskInfo(string driveLetter)
        {
            var diskInfo = HardDriveInfo.GetPartitionDiskInfo(driveLetter);
            if (diskInfo == null)
            {
                throw new System.IO.FileNotFoundException("Drive not found");
            }
            var volumeId = diskInfo.SerialNumber;

            VHDVDisk diskRecord = _db.Disks.AsNoTracking().Where(p => p.VolumeId == diskInfo.VolumeId).FirstOrDefault();
            var newRecord = (diskRecord == null);
            if (newRecord)
            {
                diskRecord = new VHDVDisk();
                diskRecord.VolumeId = volumeId;
            }
            diskRecord.Caption = diskInfo.Caption;
            diskRecord.Description = diskInfo.Description;
            diskRecord.FirmwareRevision = diskInfo.FirmwareRevision;
            diskRecord.Manufacturer = diskInfo.Manufacturer;
            diskRecord.MediaType = diskInfo.MediaType;
            diskRecord.Model = diskInfo.Model;
            diskRecord.SerialNumber = diskInfo.SerialNumber;
            diskRecord.Size = diskInfo.Size;
            diskRecord.SystemName = diskInfo.SystemName;
            diskRecord.TotalCylinders = diskInfo.TotalCylinders;
            diskRecord.TotalHeads = diskInfo.TotalHeads;
            diskRecord.TotalTracks = diskInfo.TotalTracks;
            diskRecord.TracksPerCylinder = diskInfo.TracksPerCylinder;
            diskRecord.Type = diskInfo.Type;

            if (newRecord)
                _db.Disks.Add(diskRecord);
            else
                _db.Disks.Update(diskRecord);
            _db.SaveChanges();
            _db.Entry<VHDVDisk>(diskRecord).State = EntityState.Detached;

            return diskRecord;
        }
        /// <summary>
        /// Sets the partition information.
        /// </summary>
        /// <param name="driveLetter">The drive letter.</param>
        /// <param name="diskId">The disk identifier.</param>
        /// <returns></returns>
        private VHDVPartition SetPartitionInfo(string driveLetter, int diskId)
        {
            System.IO.DriveInfo di = new System.IO.DriveInfo($"{driveLetter}\\");
            Console.WriteLine($"drive: {di.VolumeLabel}, {di.DriveFormat}, {di.DriveType}, {di.TotalSize}, {di.TotalFreeSpace}");

            var partitionInfo = HardDriveInfo.GetPartitionInfo(driveLetter);
            var partId = partitionInfo.VolumeSerialNumber;

            VHDVPartition partitionRecord = _db.Partitions.AsNoTracking().Where(p => p.PartitionId == partId).FirstOrDefault();
            var newRecord = (partitionRecord == null);
            if (newRecord)
            {
                partitionRecord = new VHDVPartition();
            }
            partitionRecord.PartitionId = partId;
            partitionRecord.DiskId = diskId;
            partitionRecord.Caption = partitionInfo.Caption;
            partitionRecord.Description = partitionInfo.Description;
            partitionRecord.DeviceID = partitionInfo.DeviceID;
            //partitionRecord.DriveType = partitionInfo.DriveType;
            partitionRecord.DriveType = di.DriveType.ToString();
            partitionRecord.FileSystem = partitionInfo.FileSystem;
            partitionRecord.MediaType = partitionInfo.MediaType;
            partitionRecord.Name = partitionInfo.Name;
            partitionRecord.SystemName = partitionInfo.SystemName;
            partitionRecord.VolumeDirty = partitionInfo.VolumeDirty;
            partitionRecord.VolumeName = partitionInfo.VolumeName;
            partitionRecord.VolumeSerialNumber = partitionInfo.VolumeSerialNumber;

            partitionRecord.Label = di.VolumeLabel;

            if (newRecord)
                _db.Partitions.Add(partitionRecord);
            else
                _db.Partitions.Update(partitionRecord);
            _db.SaveChanges();
            _db.Entry<VHDVPartition>(partitionRecord).State = EntityState.Detached;
            return partitionRecord;
        }
        /// <summary>
        /// Scans the files and folders.
        /// </summary>
        /// <param name="driveLetter">The drive letter.</param>
        private void ScanFilesAndFolders(string driveLetter, int partitionId)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            int dirLevel = 0;
            AddDirectories(driveLetter + "\\", partitionId, dirLevel);
            _db.SaveChanges();
            /*
            //IEnumerable<string> files = Directory.EnumerateFiles($"{_driveLetter}\\", "*.*", SearchOption.AllDirectories);
            //IEnumerable<string> files = Directory.EnumerateFiles($"{_driveLetter}\\");
            //IEnumerable<string> items = Directory.EnumerateDirectories($"{_driveLetter}\\");
            //IEnumerable<string> items = Directory.EnumerateFileSystemEntries($"{_driveLetter}\\");
            */
            if (_stopScan)
                OnScanStatus(new ScanStatusEventArgs("break", $"Scan stopped for drive {driveLetter}", $"Scan stopped for drive {driveLetter}, Time: {stopWatch.Elapsed.Hours}:{stopWatch.Elapsed.Minutes}:{stopWatch.Elapsed.Seconds},{stopWatch.ElapsedMilliseconds}"));
            else
                OnScanStatus(new ScanStatusEventArgs("finish", $"Scan done for drive {driveLetter}", $"Scan done for drive {driveLetter}, Time: {stopWatch.Elapsed.Hours}:{stopWatch.Elapsed.Minutes}:{stopWatch.Elapsed.Seconds},{stopWatch.ElapsedMilliseconds}"));

            stopWatch.Stop();
            Console.WriteLine($"time: {stopWatch.Elapsed.Hours}:{stopWatch.Elapsed.Minutes}:{stopWatch.Elapsed.Seconds},{stopWatch.ElapsedMilliseconds}");
        }
        #endregion

        #region AddDirectories, AddFiles
        /// <summary>
        /// Adds the directories.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="partitionId">The partition identifier.</param>
        private void AddDirectories(string path, int partitionId, int dirLevel)
        {
            dirLevel++;
            
            if (dirLevel > 2) return;

            var driveLetter = path.Substring(0, 1);
            var items = Directory.EnumerateDirectories($"{path}", "*.*", SearchOption.TopDirectoryOnly);
            try
            {
                foreach (string item in items)
                {
                    // aboard scan
                    if (_stopScan) break;

                    DirectoryInfo di = new DirectoryInfo(item);
                    var dirPath = di.Parent.FullName.Substring(2);
                    //Console.WriteLine($"dir: {item}, {di.Extension}, {di.Attributes}, {di.CreationTime}, {di.LastAccessTime}, {di.LastWriteTime}");
                    OnScanStatus(new ScanStatusEventArgs("dir", $"{item}", $"dir: {item}, {di.Extension}, {di.Attributes}, {di.CreationTime}, {di.LastAccessTime}, {di.LastWriteTime}"));  ;

                    VHDVFileSystemItem dirRecord = _db.FileSystemItems.AsNoTracking().Where(p => p.PartitionId == partitionId && p.Path == dirPath && p.Name == di.Name).FirstOrDefault();
                    var newDir = (dirRecord == null);
                    if (newDir)
                    {
                        dirRecord = new VHDVFileSystemItem
                        {
                            PartitionId = partitionId,
                            Path = dirPath,
                            Name = di.Name,
                            Type = "dir"
                        };
                    }
                    dirRecord.Level = dirLevel;
                    dirRecord.DriveLetter = driveLetter;
                    dirRecord.Attribute = di.Attributes.ToString();
                    dirRecord.Created = di.CreationTimeUtc;
                    dirRecord.LastModifiered = di.LastWriteTimeUtc;
                    dirRecord.Size = 0;
                    dirRecord.PartitionId = partitionId;

                    try
                    {
                        // add files 
                        AddFiles(di.FullName, partitionId, dirLevel);
                        // get the next directories
                        AddDirectories(di.FullName, partitionId, dirLevel);
                    }
                    catch (Exception Ex)
                    {
                        dirRecord.Comment = $"Last Error: {Ex.Message}";
                    }

                    if (newDir)
                    {
                        _db.FileSystemItems.Add(dirRecord);
                    }
                    else
                    {
                        _db.FileSystemItems.Update(dirRecord);
                        _db.Entry<VHDVFileSystemItem>(dirRecord).State = EntityState.Detached;
                    }
                }
                // add files from root
                if (path.Substring(2) == "\\")
                    AddFiles(path, partitionId, dirLevel);
            }
            catch (Exception Ex)
            {
                Console.WriteLine($"Error {Ex.Message}");
                throw;
            }
        }
        /// <summary>
        /// Adds the files.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="partitionId">The partition identifier.</param>
        private void AddFiles(string path, int partitionId, int dirLevel)
        {
            var driveLetter = path.Substring(0, 1);
            IEnumerable<string> items = Directory.EnumerateFiles($"{path}", "*.*", SearchOption.TopDirectoryOnly);
            //IEnumerable<string> items = Directory.EnumerateFiles($"{driveLetter}\\", "*.*", SearchOption.TopDirectoryOnly);
            try
            {
                Console.WriteLine("scan files...");
                foreach (string item in items)
                {
                    // aboard scan
                    if (_stopScan) break;

                    FileInfo fi = new FileInfo(item);
                    var filePath = fi.Directory.FullName.Substring(2);
                    // no output for files
                    //OnScanStatus(new ScanStatusEventArgs($"file: {item}", $"file: {item}, {fi.Extension}, {fi.Length}, {fi.Attributes}, {fi.CreationTime}, {fi.IsReadOnly}, {fi.LastAccessTime}, {fi.LastWriteTime}"));
                    //Console.WriteLine("file", $"file: {item}, {fi.Extension}, {fi.Length}, {fi.Attributes}, {fi.CreationTime}, {fi.IsReadOnly}, {fi.LastAccessTime}, {fi.LastWriteTime}");

                    VHDVFileSystemItem fileRecord = _db.FileSystemItems.AsNoTracking().Where(p => p.PartitionId == partitionId && p.Path == filePath && p.Name == fi.Name).FirstOrDefault();
                    var newFile = (fileRecord == null);
                    if (newFile)
                    {
                        fileRecord = new VHDVFileSystemItem
                        {
                            PartitionId = partitionId,
                            Path = filePath,
                            Name = fi.Name,
                            Type = "file"
                        };
                    }
                    fileRecord.Level = dirLevel;
                    fileRecord.DriveLetter = driveLetter;
                    fileRecord.Attribute = fi.Attributes.ToString();
                    fileRecord.Created = fi.CreationTimeUtc;
                    fileRecord.LastModifiered = fi.LastWriteTimeUtc;
                    fileRecord.Size = fi.Length;
                    if (newFile)
                    {
                        _db.FileSystemItems.Add(fileRecord);
                    }
                    else
                    {
                        _db.FileSystemItems.Update(fileRecord);
                        _db.Entry<VHDVFileSystemItem>(fileRecord).State = EntityState.Detached;
                    }
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine($"Error {Ex.Message}");
            }
        }
        #endregion

        #region Event Handler
        /// <summary>
        /// Occurs when [scan item].
        /// </summary>
        public event ScanStatusEventHandler ScanStatus;
        /// <summary>
        /// Raises the <see cref="E:ScanItem" /> event.
        /// </summary>
        /// <param name="e">The <see cref="ScanItemEventArgs" /> instance containing the event data.</param>
        protected virtual void OnScanStatus(ScanStatusEventArgs e)
        {
            if (ScanStatus != null) ScanStatus(this, e);
        }
        #endregion

    }


    #region ScanStatusEventArgs class
    /// <summary>
    /// ScanItemEventArgs
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class ScanStatusEventArgs : System.EventArgs
    {
        #region variables
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Type { get; set; }
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }
        /// <summary>
        /// Gets or sets the long status.
        /// </summary>
        /// <value>
        /// The long status.
        /// </value>
        public string LongStatus { get; set; }
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ScanStatusEventArgs"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="status">The status.</param>
        /// <param name="longStatus">The long status.</param>
        public ScanStatusEventArgs(string type, string status, string longStatus)
        {
            this.Type = type;
            this.Status = status;
            this.LongStatus = longStatus;
        }
        #endregion
        // Provide one or more constructors, as well as fields and
        // accessors for the arguments.
    }
    #endregion

    #region ScanStatusEventHandler Declaration
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="ScanItemEventArgs"/> instance containing the event data.</param>
    public delegate void ScanStatusEventHandler(object sender, ScanStatusEventArgs e);
    #endregion

}
