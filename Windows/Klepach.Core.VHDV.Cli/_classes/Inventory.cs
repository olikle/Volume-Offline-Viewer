using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Klepach.Core.VHDV.Cli
{
    class Inventory
    {
        AppDbContext _db;

        /// <summary>Initializes a new instance of the <see cref="Inventory" /> class.</summary>
        /// <param name="db">The database.</param>
        public Inventory(AppDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Scans the drive.
        /// </summary>
        /// <param name="driveLetter">The drive letter.</param>
        public void ScanDrive(string driveLetter)
        {
            var diskRecord = SetDiskInfo(driveLetter);
            var partitionRecord = SetPartitionInfo(driveLetter, diskRecord.Id);
            // scan the files
            ScanFilesAndFolders(driveLetter, partitionRecord.Id);
        }
        /// <summary>
        /// Sets the disk information.
        /// </summary>
        /// <param name="driveLetter">The drive letter.</param>
        /// <returns></returns>
        private VOVDisk SetDiskInfo(string driveLetter)
        {
            var diskInfo = HardDriveInfo.GetPartitionDiskInfo(driveLetter);

            var volumeId = diskInfo.SerialNumber;

            VOVDisk diskRecord = _db.vOVDisks.AsNoTracking().Where(p => p.VolumeId == diskInfo.VolumeId).FirstOrDefault();
            var newRecord = (diskRecord == null);
            if (newRecord)
            {
                diskRecord = new VOVDisk();
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
                _db.vOVDisks.Add(diskRecord);
            else
                _db.vOVDisks.Update(diskRecord);
            _db.SaveChanges();

            return diskRecord;
        }
        /// <summary>
        /// Sets the partition information.
        /// </summary>
        /// <param name="driveLetter">The drive letter.</param>
        /// <param name="diskId">The disk identifier.</param>
        /// <returns></returns>
        private VOVPartition SetPartitionInfo(string driveLetter, int diskId)
        {
            System.IO.DriveInfo di = new System.IO.DriveInfo($"{driveLetter}\\");
            Console.WriteLine($"drive: {di.VolumeLabel}, {di.DriveFormat}, {di.DriveType}, {di.TotalSize}, {di.TotalFreeSpace}");

            var partitionInfo = HardDriveInfo.GetPartitionInfo(driveLetter);
            var partId = partitionInfo.VolumeSerialNumber;

            VOVPartition partitionRecord = _db.vOVPartitions.AsNoTracking().Where(p => p.PartitionId == partId).FirstOrDefault();
            var newRecord = (partitionRecord == null);
            if (newRecord)
            {
                partitionRecord = new VOVPartition();
                partitionRecord.PartitionId = partId;
            }
            partitionRecord.DiskId = diskId;
            partitionRecord.Caption = partitionInfo.Caption;
            partitionRecord.Description = partitionInfo.Description;
            partitionRecord.DeviceID = partitionInfo.DeviceID;
            partitionRecord.DriveType = partitionInfo.DriveType;
            partitionRecord.FileSystem = partitionInfo.FileSystem;
            partitionRecord.MediaType = partitionInfo.MediaType;
            partitionRecord.Name = partitionInfo.Name;
            partitionRecord.SystemName = partitionInfo.SystemName;
            partitionRecord.VolumeDirty = partitionInfo.VolumeDirty;
            partitionRecord.VolumeName = partitionInfo.VolumeName;
            partitionRecord.VolumeSerialNumber = partitionInfo.VolumeSerialNumber;

            if (newRecord)
                _db.vOVPartitions.Add(partitionRecord);
            else
                _db.vOVPartitions.Update(partitionRecord);
            _db.SaveChanges();
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
            /*
            //IEnumerable<string> files = Directory.EnumerateFiles($"{_driveLetter}\\", "*.*", SearchOption.AllDirectories);
            //IEnumerable<string> files = Directory.EnumerateFiles($"{_driveLetter}\\");
            //IEnumerable<string> items = Directory.EnumerateDirectories($"{_driveLetter}\\");
            //IEnumerable<string> items = Directory.EnumerateFileSystemEntries($"{_driveLetter}\\");

            IEnumerable<string> items = Directory.EnumerateFiles($"{driveLetter}\\", "*.*", SearchOption.AllDirectories);
            //IEnumerable<string> items = Directory.EnumerateFiles($"{driveLetter}\\", "*.*", SearchOption.TopDirectoryOnly);
            try
            {
                foreach (string item in items)
                {
                    FileInfo fi = new FileInfo(item);
                    var filePath = fi.Directory.FullName.Substring(2);
                    Console.WriteLine($"file: {item}, {fi.Extension}, {fi.Length}, {fi.Attributes}, {fi.CreationTime}, {fi.IsReadOnly}, {fi.LastAccessTime}, {fi.LastWriteTime}");

                    VOVFileSystemItem fileRecord = _db.vOVFileSystemItems.AsNoTracking().Where(p => p.PartitionId == partitionId && p.Path == filePath && p.Name == fi.Name).FirstOrDefault();
                    var newFile = (fileRecord == null);
                    if (newFile)
                    {
                        fileRecord = new VOVFileSystemItem();
                        fileRecord.PartitionId = partitionId;
                        fileRecord.Path = filePath;
                        fileRecord.Name = fi.Name;
                        fileRecord.Type = "file";
                    }
                    fileRecord.DriveLetter = driveLetter;
                    fileRecord.Attribute = fi.Attributes.ToString();
                    fileRecord.Created = fi.CreationTimeUtc;
                    fileRecord.LastModifiered = fi.LastWriteTimeUtc;
                    fileRecord.Size = fi.Length;
                    if (newFile)
                        _db.vOVFileSystemItems.Add(fileRecord);
                    else
                        _db.vOVFileSystemItems.Update(fileRecord);
                }
            }catch(Exception Ex)
            {
                Console.WriteLine($"Error {Ex.Message}");
            }
            //items = Directory.EnumerateDirectories($"{driveLetter}\\", "*.*", SearchOption.TopDirectoryOnly);
            items = Directory.EnumerateDirectories($"{driveLetter}\\", "*.*", SearchOption.AllDirectories);
            foreach (string item in items)
            {
                DirectoryInfo di = new DirectoryInfo(item);                
                var dirPath = di.Parent.FullName.Substring(2);
                Console.WriteLine($"file: {item}, {di.Extension}, {di.Attributes}, {di.CreationTime}, {di.LastAccessTime}, {di.LastWriteTime}");

                VOVFileSystemItem dirRecord = _db.vOVFileSystemItems.AsNoTracking().Where(p => p.PartitionId == partitionId && p.Path == dirPath && p.Name == di.Name).FirstOrDefault();
                var newDir = (dirRecord == null);
                if (newDir)
                {
                    dirRecord = new VOVFileSystemItem();
                    dirRecord.PartitionId = partitionId;
                    dirRecord.Path = dirPath;
                    dirRecord.Name = di.Name;
                    dirRecord.Type = "dir";
                }
                dirRecord.DriveLetter = driveLetter;
                dirRecord.Attribute = di.Attributes.ToString();
                dirRecord.Created = di.CreationTimeUtc;
                dirRecord.LastModifiered = di.LastWriteTimeUtc;
                dirRecord.Size = 0;
                dirRecord.PartitionId = partitionId;
                if (newDir)
                    _db.vOVFileSystemItems.Add(dirRecord);
                else
                    _db.vOVFileSystemItems.Update(dirRecord);
            }
            */
            _db.SaveChanges();

            stopWatch.Stop();
            Console.WriteLine($"time: {stopWatch.Elapsed.Hours}:{stopWatch.Elapsed.Minutes}:{stopWatch.Elapsed.Seconds},{stopWatch.ElapsedMilliseconds}");
        }
        /// <summary>
        /// Adds the directories.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="partitionId">The partition identifier.</param>
        private void AddDirectories(string path, int partitionId, int dirLevel)
        {
            dirLevel++;
            var driveLetter = path.Substring(0, 1);
            var items = Directory.EnumerateDirectories($"{path}", "*.*", SearchOption.TopDirectoryOnly);
            try
            {
                foreach (string item in items)
                {
                    DirectoryInfo di = new DirectoryInfo(item);
                    var dirPath = di.Parent.FullName.Substring(2);
                    Console.WriteLine($"file: {item}, {di.Extension}, {di.Attributes}, {di.CreationTime}, {di.LastAccessTime}, {di.LastWriteTime}");

                    VOVFileSystemItem dirRecord = _db.vOVFileSystemItems.AsNoTracking().Where(p => p.PartitionId == partitionId && p.Path == dirPath && p.Name == di.Name).FirstOrDefault();
                    var newDir = (dirRecord == null);
                    if (newDir)
                    {
                        dirRecord = new VOVFileSystemItem
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
                    if (newDir)
                        _db.vOVFileSystemItems.Add(dirRecord);
                    else
                        _db.vOVFileSystemItems.Update(dirRecord);

                    // add files 
                    AddFiles(di.FullName, partitionId, dirLevel);
                    // get the next directories
                    AddDirectories(di.FullName, partitionId, dirLevel);
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine($"Error {Ex.Message}");
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
                foreach (string item in items)
                {
                    FileInfo fi = new FileInfo(item);
                    var filePath = fi.Directory.FullName.Substring(2);
                    Console.WriteLine($"file: {item}, {fi.Extension}, {fi.Length}, {fi.Attributes}, {fi.CreationTime}, {fi.IsReadOnly}, {fi.LastAccessTime}, {fi.LastWriteTime}");

                    VOVFileSystemItem fileRecord = _db.vOVFileSystemItems.AsNoTracking().Where(p => p.PartitionId == partitionId && p.Path == filePath && p.Name == fi.Name).FirstOrDefault();
                    var newFile = (fileRecord == null);
                    if (newFile)
                    {
                        fileRecord = new VOVFileSystemItem
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
                        _db.vOVFileSystemItems.Add(fileRecord);
                    else
                        _db.vOVFileSystemItems.Update(fileRecord);
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine($"Error {Ex.Message}");
            }
        }


    }
}
