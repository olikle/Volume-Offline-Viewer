using Klepach.Core.VHDV.Db;
using System;
using System.Data;
using System.IO;

namespace Klepach.Core.VHDV.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDbContext db = new AppDbContext();

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

                Inventory inventory = new Inventory(db);
                inventory.ScanDrive(driveLetter);

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
        }
    }
}
