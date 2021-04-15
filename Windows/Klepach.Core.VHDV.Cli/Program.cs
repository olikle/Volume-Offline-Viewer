using System;
using System.Data;
using System.IO;

namespace Klepach.Core.VHDV.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            string databaseName = @"C:\Data\AceWare\GitProjects\Klepach.Core.VHDV.Cli\harddisks.vhdv";
            if (!File.Exists(databaseName))
                databaseName = @"harddisks.vhdv";

            Database db = new Database(databaseName);

            string command = "listvolumes";
            if (args.Length > 0)
                command = args[0];

            if (command == "scandrive")
            {
                string driveLetter = "C:";
                if (args.Length > 1)
                    driveLetter = args[1];
                if (!driveLetter.Contains(":")) driveLetter += ":";

                string volumeId = null;
                if (args.Length > 2)
                    volumeId = args[2];

                var partitionInfo = HardDriveInfo.GetPartitionInfo(driveLetter);
                /*
                // add drive info to database
                var hdNo = HardDriveInfo.GetDiskIDFromDriveLetter(driveLetter);
                var hdVolume = HardDriveInfo.GetInfo(driveLetter, volumeId);
                if (string.IsNullOrEmpty(hdVolume.SerialNumber))
                {
                    Console.WriteLine("*** No Serial Number for drive. ***");
                    Console.WriteLine($"Caption: {hdVolume.Caption}");
                    Console.WriteLine($"Description: {hdVolume.Description}");
                    Console.WriteLine($"FirmwareRevision: {hdVolume.FirmwareRevision}");
                    Console.WriteLine($"Manufacturer: {hdVolume.Manufacturer}");
                    Console.WriteLine($"MediaType: {hdVolume.MediaType}");
                    Console.WriteLine($"Model: {hdVolume.Model}");
                    Console.WriteLine($"SystemName: {hdVolume.SystemName}");
                    return;
                }
                db.InsertVolumesItem(hdVolume);
                */
            }
            return;
            if (command == "listvolumes" || command == "scandrive")
            {
                // get stored volumnes
                DataTable dsVolumes = db.GetVolumes();
                int dsVolumesCount = dsVolumes.Rows.Count;
                Console.WriteLine($"stored drives: {dsVolumesCount}");
                foreach (DataRow dataRow in dsVolumes.Rows)
                {
                    Console.WriteLine("\n\nDrive");
                    foreach (DataColumn col in dataRow.Table.Columns)
                    {
                        Console.WriteLine($"{col.ColumnName}: {dataRow[col.ColumnName]}");
                    }
                }
            }

            /*
            Inventory driveInventory = new Inventory(driveLetter);
            //Inventory driveInventory = new Inventory("C:");
            driveInventory.GetDriveInfo();
            //driveInventory.ScanFilesAndFolders();
            //Console.WriteLine(HardDriveInfo.GetHardDiskDSerialNumber("C:"));

            */
        }
    }
}
