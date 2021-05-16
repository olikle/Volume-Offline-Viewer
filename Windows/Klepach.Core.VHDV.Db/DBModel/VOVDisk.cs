using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klepach.Core.VHDV.Db
{
    /// <summary>
    /// VOVDisk
    /// </summary>
    public class VOVDisk
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


        public IEnumerable<VOVPartition> Partitions { get; set; }

        public override string ToString()
        {
            return $"{VolumeId} ({Id})";
        }
    }
}
