using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using System.Data;

namespace Klepach.Core.VHDV.Cli

{
    class Database
    {
        string _databaseName = "";
        SQLiteConnection dbConnection = null;
        //https://zetcode.com/csharp/sqlite/
        public Database(string databaseName)
        {
            _databaseName = databaseName;
            var initTable = File.Exists(_databaseName);
            if (!initTable)
                SQLiteConnection.CreateFile(_databaseName);
            dbConnection = new SQLiteConnection($"Data Source={_databaseName}; Version=3;");
            //using var dbConnection = new SQLiteConnection($"{_databaseName}");
            dbConnection.Open();

            if (!initTable)
            {
                CreateTable();
            }
        }
        public void InsertVolumesItem(Disk insertVolume)
        {
            DataTable foundVolume = GetVolume(insertVolume.SerialNumber);
            if (foundVolume.Rows.Count > 0)
            {
                Console.WriteLine($"Volume '{insertVolume.SerialNumber}' exists.");
                return;
            }
            using var cmd = new SQLiteCommand(dbConnection);
            string insertSql = "INSERT INTO volumes(SerialNumber, Caption, Description, FirmwareRevision, Manufacturer, MediaType, Model, Size, SystemName, TotalCylinders, TotalHeads, TotalSectors, TotalTracks, TracksPerCylinder)";
            insertSql += $" VALUES ('{insertVolume.SerialNumber}', '{insertVolume.Caption}', '{insertVolume.Description}', '{insertVolume.FirmwareRevision}', '{insertVolume.Manufacturer}', '{insertVolume.MediaType}', '{insertVolume.Model}', {insertVolume.Size}, '{insertVolume.SystemName}', {insertVolume.TotalCylinders}, {insertVolume.TotalHeads}, {insertVolume.TotalSectors}, {insertVolume.TotalTracks}, {insertVolume.TracksPerCylinder})";
            cmd.CommandText = insertSql;
            cmd.ExecuteNonQuery();
        }

        public DataTable GetVolume(string serialNumber)
        {
            var args = new Dictionary<string, object>
            {
                {"@SerialNumber", serialNumber}
            };
            return Execute("SELECT * FROM volumes WHERE SerialNumber=@SerialNumber", args);
        }

        public DataTable GetVolumes()
        {
            return Execute("SELECT * FROM volumes");

            //DataSet returnDataSet = new DataSet();
            //using var cmd = new SQLiteCommand(dbConnection);

            //cmd.CommandText = "SELECT * FROM volumes";
            ////returnDataSet = cmd.ExecuteReader();
            
            //using SQLiteDataReader rdr = cmd.ExecuteReader();

            //while (rdr.Read())
            //{
            //    Console.WriteLine($"{rdr.GetInt32(0)} {rdr.GetString(1)} {rdr.GetInt32(2)}");
            //}


            //return returnDataSet;
        }

        private DataTable Execute(string query, Dictionary<string, object> args = null)
        {
            if (string.IsNullOrEmpty(query.Trim()))
                return null;
            //var args = new Dictionary<string, object>
            //{
            //    {"@id", id}
            //};
            using (var cmd = new SQLiteCommand(query, dbConnection))
            {
                if (args != null)
                {
                    foreach (KeyValuePair<string, object> entry in args)
                    {
                        cmd.Parameters.AddWithValue(entry.Key, entry.Value);
                    }
                }

                var da = new SQLiteDataAdapter(cmd);

                var dt = new DataTable();
                da.Fill(dt);

                da.Dispose();
                return dt;
            }
        }

        public void CreateTable()
        {
            using var cmd = new SQLiteCommand(dbConnection);

            cmd.CommandText = "DROP TABLE IF EXISTS volumes";
            cmd.ExecuteNonQuery();

            cmd.CommandText = @"CREATE TABLE volumes(id INTEGER PRIMARY KEY, SerialNumber TEXT, Caption TEXT, Description TEXT, FirmwareRevision TEXT, Manufacturer TEXT, MediaType TEXT, Model TEXT, Size INT, SystemName TEXT, TotalCylinders INT, TotalHeads INT, TotalSectors INT, TotalTracks INT, TracksPerCylinder INT)";
            cmd.ExecuteNonQuery();

            Console.WriteLine("Table volumes created");
        }
    }
}
