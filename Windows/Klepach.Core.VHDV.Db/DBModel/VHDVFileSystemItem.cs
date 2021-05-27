using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klepach.Core.VHDV.Db
{
    /// <summary>
    /// VHDVFileSystemItem
    /// </summary>
    public class VHDVFileSystemItem
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string DriveLetter { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public string Attribute { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModifiered { get; set; }
        public long Size { get; set; }
        public string Comment{ get; set; }

        public int? PartitionId { get; set; }
        public VHDVPartition Partition { get; set; }


        public override string ToString()
        {
            return $"{(Path != "/" ? Path : "")}/{Name} ({Id})";
        }
    }
}
