using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klepach.Core.VHDV.Cli
{
    class VOVFile
    {
        public int Id{ get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModifiered { get; set; }
        public long Size { get; set; }

        public int? PartitionId { get; set; }
        public VOVPartition Partition { get; set; }

        public override string ToString()
        {
            return $"{FilePath}/{FileName} ({Id})";
        }
    }
}
