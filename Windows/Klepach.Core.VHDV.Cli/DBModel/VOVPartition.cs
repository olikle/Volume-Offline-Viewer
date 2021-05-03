using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klepach.Core.VHDV.Cli
{
    class VOVPartition
    {
        public int Id { get; set; }
        //public Dictionary<string, object> RawRroperties { get; set; }
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

        public int? DiskId { get; set; }
        public VOVDisk Disk { get; set; }

        public IEnumerable<VOVFile> Files { get; set; }

        public override string ToString()
        {
            return $"{Name} ({Id})";
        }
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
}
