using System;
using System.Collections.Generic;
using System.Text;
using System.Management;

namespace Klepach.Core.VHDV.Cli
{
    class HardDriveInfo
    {
        #region PartitionInfo

        /// <summary>
        /// Gets the partition information.
        /// </summary>
        /// <param name="driveLetter">The drive letter.</param>
        /// <returns></returns>
        public static Partition GetPartitionInfo(string driveLetter)
        {
            Partition returnPartition = new Partition();
            //Create our ManagementObject, passing it the drive letter to the
            //DevideID using WQL
            ManagementObject hardPartition = new ManagementObject("Win32_LogicalDisk.DeviceID=\"" + driveLetter + "\"");
            //bind our management object
            hardPartition.Get();

            Console.WriteLine("--------- Found Partition -----------");
            ListProperties(hardPartition);


            returnPartition.Caption = GetPropertyValue(hardPartition, "Caption") as string;
            returnPartition.DeviceID = GetPropertyValue(hardPartition, "DeviceID") as string;
            returnPartition.Description = GetPropertyValue(hardPartition, "Description") as string;
            returnPartition.FileSystem = GetPropertyValue(hardPartition, "FileSystem") as string;
            returnPartition.Name = GetPropertyValue(hardPartition, "Name") as string;
            returnPartition.SystemName = GetPropertyValue(hardPartition, "SystemName") as string;
            returnPartition.VolumeName = GetPropertyValue(hardPartition, "VolumeName") as string;
            returnPartition.VolumeSerialNumber = GetPropertyValue(hardPartition, "VolumeSerialNumber") as string;

            returnPartition.DriveType = Convert.ToInt16(GetPropertyValue(hardPartition, "DriveType"));
            returnPartition.MediaType = Convert.ToInt16(GetPropertyValue(hardPartition, "MediaType"));
            returnPartition.VolumeDirty = Convert.ToBoolean(GetPropertyValue(hardPartition, "VolumeDirty"));

            returnPartition.ParentDisk = GetPartitionDiskInfo(driveLetter);
            returnPartition.RawRroperties = GetPropertiesAsDic(hardPartition);
            /*

            Dictionary<string, object> partProperties = new Dictionary<string, object>();
            foreach (PropertyData prop in hardPartition.Properties)
            {
                string value = "";
                int intValue = 0;
                if (hardPartition[prop.Name] != null)
                {
                    value = hardPartition[prop.Name].ToString();
                    int.TryParse(hardPartition[prop.Name].ToString(), out intValue);
                }
                
                if (prop.Name == "DeviceID") returnPartition.DeviceID = value;
                if (prop.Name == "Caption") returnPartition.Caption = value;
                if (prop.Name == "Description") returnPartition.Description = value;
                if (prop.Name == "DriveType") returnPartition.DriveType = intValue;
                if (prop.Name == "FileSystem") returnPartition.FileSystem = value;
                if (prop.Name == "MediaType") returnPartition.MediaType = intValue;
                if (prop.Name == "Name") returnPartition.Name = value;
                if (prop.Name == "SystemName") returnPartition.SystemName = value;
                if (prop.Name == "VolumeDirty") returnPartition.VolumeDirty = (bool)hardPartition[prop.Name];
                if (prop.Name == "VolumeName") returnPartition.VolumeName = value;
                if (prop.Name == "VolumeSerialNumber") returnPartition.VolumeSerialNumber = value;

                if (prop.Type == CimType.UInt8 || prop.Type == CimType.UInt16 || prop.Type == CimType.UInt32 || prop.Type == CimType.UInt64)
                    partProperties.Add(prop.Name, intValue);
                else
                    partProperties.Add(prop.Name, value);
            }
            returnPartition.ParentDisk = GetPartitionDiskInfo(driveLetter);
            returnPartition.RawRroperties = partProperties;
            */

            /*
Access = 0
Availability = null
BlockSize = null
Caption = C:
Compressed = False
ConfigManagerErrorCode = null
ConfigManagerUserConfig = null
CreationClassName = Win32_LogicalDisk
Description = Lokale Festplatte
DeviceID = C:
DriveType = 3
ErrorCleared = null
ErrorDescription = null
ErrorMethodology = null
FileSystem = NTFS
FreeSpace = 105665662976
InstallDate = null
LastErrorCode = null
MaximumComponentLength = 255
MediaType = 12
Name = C:
NumberOfBlocks = null
PNPDeviceID = null
PowerManagementCapabilities = null
PowerManagementSupported = null
ProviderName = null
Purpose = null
QuotasDisabled = True
QuotasIncomplete = False
QuotasRebuilding = False
Size = 171189465088
Status = null
StatusInfo = null
SupportsDiskQuotas = True
SupportsFileBasedCompression = True
SystemCreationClassName = Win32_ComputerSystem
SystemName = OLIWIN10TEST
VolumeDirty = False
VolumeName =
VolumeSerialNumber = 30A2032B            
             */
            return returnPartition;
        }
        #endregion

        #region GetPartitionDiskInfo

        /// <summary>
        /// https://social.msdn.microsoft.com/Forums/en-US/923ccb2e-857e-429d-90ae-00f73e32eba4/getting-usb-flash-drive-serial-number-in-c?forum=wdk
        /// </summary>
        /// <param name="driveLetter"></param>
        /// <returns></returns>
        public static Disk GetPartitionDiskInfo(string driveLetter)
        {
            Disk returnDisk = new Disk();
            string diskNo = "";

            ManagementObjectSearcher diskToPartitions = new ManagementObjectSearcher("SELECT * FROM Win32_LogicalDiskToPartition");
            foreach (ManagementObject diskToPartition in diskToPartitions.Get())
            {
                /*
                 Antecedent = \\OLIWIN10TEST\root\cimv2:Win32_DiskPartition.DeviceID="Disk #0, Partition #1"
                 Dependent = \\OLIWIN10TEST\root\cimv2:Win32_LogicalDisk.DeviceID="C:"
                 EndingAddress = 171797643263
                 StartingAddress = 608174080
                */

                // look for the correspending drive letter
                if (driveLetter != GetValueInQuotes(diskToPartition["Dependent"].ToString()))
                    continue;
                diskNo = GetValueInQuotes(diskToPartition["Antecedent"].ToString());
                diskNo = diskNo.Substring(0, diskNo.IndexOf(",", StringComparison.OrdinalIgnoreCase));
                diskNo = diskNo.Substring(diskNo.IndexOf("#", StringComparison.OrdinalIgnoreCase)+1);

                Console.WriteLine("--------- Found Disk/Partition -----------");
                ListProperties(diskToPartition);
            }

            if (string.IsNullOrEmpty(diskNo))
                return null;

            ManagementObjectSearcher disks = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");
            foreach (ManagementObject disk in disks.Get())
            {
                if (disk["DeviceId"].ToString() != ("\\\\.\\PHYSICALDRIVE" + diskNo))
                    continue;

                returnDisk.Caption = GetPropertyValue(disk, "Caption") as string;
                returnDisk.Description = GetPropertyValue(disk, "Description") as string;
                returnDisk.MediaType = GetPropertyValue(disk, "MediaType") as string;
                returnDisk.Model = GetPropertyValue(disk, "Model") as string;
                returnDisk.SerialNumber = GetPropertyValue(disk, "SerialNumber") as string;
                returnDisk.Manufacturer = GetPropertyValue(disk, "Manufacturer") as string;

                returnDisk.Size = Convert.ToInt64( GetPropertyValue(disk, "Size") );
                returnDisk.TotalCylinders = Convert.ToInt64( GetPropertyValue(disk, "TotalCylinders") );
                returnDisk.TotalHeads = Convert.ToInt64( GetPropertyValue(disk, "TotalHeads") );
                returnDisk.TotalSectors = Convert.ToInt64( GetPropertyValue(disk, "TotalSectors") );
                returnDisk.TotalTracks = Convert.ToInt64( GetPropertyValue(disk, "TotalTracks") );
                returnDisk.TracksPerCylinder = Convert.ToInt64( GetPropertyValue(disk, "TracksPerCylinder") );


                Console.WriteLine("--------- Found Disk -----------");
                ListProperties(disk);

                //{
                //    //this._serialNumber = parseSerialFromDeviceID(disk["PNPDeviceID"].ToString());
                //}
            }
            return returnDisk;
        }
        #endregion

        /// <summary>
        /// Gets the information.
        /// </summary>
        /// <param name="driveLetter">The drive letter.</param>
        /// <param name="volumeId">The volume identifier.</param>
        /// <returns></returns>
        public static Disk GetInfo(string driveLetter, string volumeId)
        {
            Disk returnVolume = new Disk();
            returnVolume.VolumeId = volumeId;
            ManagementObjectSearcher mangObjSearch = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");

            foreach (ManagementObject mangObj in mangObjSearch.Get())
            {
                Console.WriteLine($"{mangObj["Name"]}");
                if (mangObj["Name"].ToString() == ("\\\\.\\PHYSICALDRIVE" + driveLetter))
                {
                    foreach (PropertyData prop in mangObj.Properties)
                    {
                        string value = "";
                        int intValue = 0;
                        if (mangObj[prop.Name] != null)
                        {
                            value = mangObj[prop.Name].ToString();
                            int.TryParse(mangObj[prop.Name].ToString(), out intValue);
                        }
                        if (prop.Name == "Caption") returnVolume.Caption = value;
                        if (prop.Name == "Description") returnVolume.Description = value;
                        if (prop.Name == "MediaType") returnVolume.Type = value;
                        if (prop.Name == "Model") returnVolume.Model = value;
                        if (prop.Name == "SerialNumber") returnVolume.SerialNumber = value;
                        if (prop.Name == "Size") returnVolume.Size = intValue;
                        if (prop.Name == "SystemName") returnVolume.SystemName = value;
                        if (prop.Name == "TotalCylinders") returnVolume.TotalCylinders = intValue;
                        if (prop.Name == "TotalHeads") returnVolume.TotalHeads = intValue;
                        if (prop.Name == "TotalSectors") returnVolume.TotalSectors = intValue;
                        if (prop.Name == "TotalTracks") returnVolume.TotalTracks = intValue;
                        if (prop.Name == "TracksPerCylinder") returnVolume.TracksPerCylinder = intValue;

                        if (mangObj[prop.Name] != null)
                            Console.WriteLine($"{prop.Name} = {mangObj[prop.Name].ToString()}");
                        else
                            Console.WriteLine($"{prop.Name} = null");
                    }
                }
            }
            if (string.IsNullOrEmpty(returnVolume.SerialNumber) && string.IsNullOrEmpty(volumeId))
                returnVolume.SerialNumber = volumeId;

            if (string.IsNullOrEmpty(returnVolume.SerialNumber))
            {
                if (returnVolume.Model == "Virtual HD ATA Device")
                {
                    returnVolume.SerialNumber = $"{returnVolume.SystemName}-{driveLetter}";
                }
            }
            return returnVolume;
        }
        /// <summary>
        /// GetValueInQuotes
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static string GetValueInQuotes(string value)
        {
            string returnValue = value.Substring(value.IndexOf("\"", StringComparison.OrdinalIgnoreCase)+1);
            returnValue = returnValue.Substring(0, returnValue.IndexOf("\"",StringComparison.OrdinalIgnoreCase));

            return returnValue;
        }
        /// <summary>
        /// ListProperties
        /// </summary>
        /// <param name="mngObj"></param>
        private static void ListProperties(ManagementObject mngObj)
        {
            foreach (PropertyData prop in mngObj.Properties)
            {
                if (mngObj[prop.Name] != null)
                    Console.WriteLine($"{prop.Name} = {mngObj[prop.Name].ToString()}");
                else
                    Console.WriteLine($"{prop.Name} = null");
            }
            Console.WriteLine("");
        }
        /// <summary>
        /// Get Properties As Dictionary
        /// </summary>
        /// <param name="mngObj"></param>
        private static Dictionary<string, object> GetPropertiesAsDic(ManagementObject mngObj)
        {
            Dictionary<string, object> partProperties = new Dictionary<string, object>();
            foreach (PropertyData prop in mngObj.Properties)
            {
                partProperties.Add(prop.Name, mngObj[prop.Name]);
            }
            return partProperties;
        }
        /// <summary>
        /// GetPropertyValue
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mngObj"></param>
        /// <param name="propName"></param>
        /// <returns></returns>
        private static object GetPropertyValue(ManagementObject mngObj, string propName)
        {
            if (mngObj[propName] != null)
                return mngObj[propName];
            else
                return null;
        }
    }
    /// <summary>
    /// Partition Object
    /// </summary>
    class Partition
    {
        public int Id { get; set; }
        public Dictionary<string, object> RawRroperties { get; set; }
        public string DeviceID { get; set; }
        public string Caption { get; set; }
        public string Description { get; set; }
        public int DriveType { get; set; }
        public string FileSystem { get; set; }
        public int MediaType { get; set; }
        public string Name { get; set; }
        public string SystemName { get; set; }
        public bool VolumeDirty { get; set; }
        public string VolumeName { get; set; }
        public string VolumeSerialNumber { get; set; }

        public Disk ParentDisk { get; set; }
        /*
Access = 0
Availability = null
BlockSize = null
Caption = C:
Compressed = False
ConfigManagerErrorCode = null
ConfigManagerUserConfig = null
CreationClassName = Win32_LogicalDisk
Description = Lokale Festplatte
DeviceID = C:
DriveType = 3
ErrorCleared = null
ErrorDescription = null
ErrorMethodology = null
FileSystem = NTFS
FreeSpace = 105665662976
InstallDate = null
LastErrorCode = null
MaximumComponentLength = 255
MediaType = 12
Name = C:
NumberOfBlocks = null
PNPDeviceID = null
PowerManagementCapabilities = null
PowerManagementSupported = null
ProviderName = null
Purpose = null
QuotasDisabled = True
QuotasIncomplete = False
QuotasRebuilding = False
Size = 171189465088
Status = null
StatusInfo = null
SupportsDiskQuotas = True
SupportsFileBasedCompression = True
SystemCreationClassName = Win32_ComputerSystem
SystemName = OLIWIN10TEST
VolumeDirty = False
VolumeName =
VolumeSerialNumber = 30A2032B            
         */
    }
    class Disk
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string VolumeId { get; set; }
        public string SerialNumber { get; set; }
        public string Caption { get; set; }
        public string Description { get; set; }
        public string FirmwareRevision { get; set; }
        public string Manufacturer { get; set; }
        public string MediaType { get; set; }
        public string Model { get; set; }
        public long Size { get; set; }
        public string SystemName { get; set; }
        public long TotalCylinders { get; set; }
        public long TotalHeads { get; set; }
        public long TotalSectors { get; set; }
        public long TotalTracks { get; set; }
        public long TracksPerCylinder { get; set; }
    }
}
