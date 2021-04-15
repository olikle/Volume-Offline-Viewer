using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public void GetDriveInfo()
        {
            System.IO.DriveInfo di = new System.IO.DriveInfo($"{_driveLetter}\\");
            Console.WriteLine($"drive: {di.VolumeLabel}, {di.DriveFormat}, {di.DriveType}, {di.TotalSize}, {di.TotalFreeSpace}");

        }
        public void ScanFilesAndFolders()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            //IEnumerable<string> files = Directory.EnumerateFiles($"{_driveLetter}\\", "*.*", SearchOption.AllDirectories);
            //IEnumerable<string> files = Directory.EnumerateFiles($"{_driveLetter}\\");
            //IEnumerable<string> items = Directory.EnumerateDirectories($"{_driveLetter}\\");
            //IEnumerable<string> items = Directory.EnumerateFileSystemEntries($"{_driveLetter}\\");
            IEnumerable<string> items = Directory.EnumerateFiles($"{_driveLetter}\\", "*.*", SearchOption.TopDirectoryOnly);
            foreach (string item in items)
            {
                FileInfo fi = new FileInfo(item);
                Console.WriteLine($"file: {item}, {fi.Extension}, {fi.Length}, {fi.Attributes}, {fi.CreationTime}, {fi.IsReadOnly}, {fi.LastAccessTime}, {fi.LastWriteTime}");
            }

            items = Directory.EnumerateDirectories($"{_driveLetter}\\", "*.*", SearchOption.TopDirectoryOnly);
            foreach (string item in items)
            {
                DirectoryInfo di = new DirectoryInfo(item);
                Console.WriteLine($"file: {item}, {di.Extension}, {di.Attributes}, {di.CreationTime}, {di.LastAccessTime}, {di.LastWriteTime}");
            }

            stopWatch.Stop();
            Console.WriteLine($"time: {stopWatch.Elapsed.Hours}:{stopWatch.Elapsed.Minutes}:{stopWatch.Elapsed.Seconds},{stopWatch.ElapsedMilliseconds}");

        }
    }
}
