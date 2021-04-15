using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Klepach.Core.VHDV.Cli
{
    class Inventory
    {
        string _driveLetter = "";
        public Inventory(string driveLetter)
        {
            _driveLetter = driveLetter;
        }
        public void ScanFilesAndFolders()
        {
            foreach (string path in Directory.EnumerateFiles($"{_driveLetter}\\"))
            {
                Console.WriteLine("IN C DIRECTORY: " + path);
            }
        }
    }
}
