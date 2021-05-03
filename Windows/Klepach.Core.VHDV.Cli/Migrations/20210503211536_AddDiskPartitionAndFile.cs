using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Klepach.Core.VHDV.Cli.Migrations
{
    public partial class AddDiskPartitionAndFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VOVDisk",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<string>(type: "TEXT", nullable: true),
                    volumeId = table.Column<string>(type: "TEXT", nullable: true),
                    SerialNumber = table.Column<string>(type: "TEXT", nullable: true),
                    Caption = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    FirmwareRevision = table.Column<string>(type: "TEXT", nullable: true),
                    Manufacturer = table.Column<string>(type: "TEXT", nullable: true),
                    MediaType = table.Column<string>(type: "TEXT", nullable: true),
                    Model = table.Column<string>(type: "TEXT", nullable: true),
                    Size = table.Column<long>(type: "INTEGER", nullable: false),
                    SystemName = table.Column<string>(type: "TEXT", nullable: true),
                    TotalCylinders = table.Column<long>(type: "INTEGER", nullable: false),
                    TotalHeads = table.Column<long>(type: "INTEGER", nullable: false),
                    TotalSectors = table.Column<long>(type: "INTEGER", nullable: false),
                    TotalTracks = table.Column<long>(type: "INTEGER", nullable: false),
                    TracksPerCylinder = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VOVDisk", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VOVPartition",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DeviceID = table.Column<string>(type: "TEXT", nullable: true),
                    Caption = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    DriveType = table.Column<int>(type: "INTEGER", nullable: false),
                    FileSystem = table.Column<string>(type: "TEXT", nullable: true),
                    MediaType = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    SystemName = table.Column<string>(type: "TEXT", nullable: true),
                    VolumeDirty = table.Column<bool>(type: "INTEGER", nullable: false),
                    VolumeName = table.Column<string>(type: "TEXT", nullable: true),
                    VolumeSerialNumber = table.Column<string>(type: "TEXT", nullable: true),
                    DiskId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VOVPartition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VOVPartition_VOVDisk_DiskId",
                        column: x => x.DiskId,
                        principalTable: "VOVDisk",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "vOVFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FilePath = table.Column<string>(type: "TEXT", nullable: true),
                    FileName = table.Column<string>(type: "TEXT", nullable: true),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastModifiered = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Size = table.Column<long>(type: "INTEGER", nullable: false),
                    PartitionId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vOVFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_vOVFiles_VOVPartition_PartitionId",
                        column: x => x.PartitionId,
                        principalTable: "VOVPartition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_vOVFiles_PartitionId",
                table: "vOVFiles",
                column: "PartitionId");

            migrationBuilder.CreateIndex(
                name: "IX_VOVPartition_DiskId",
                table: "VOVPartition",
                column: "DiskId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "vOVFiles");

            migrationBuilder.DropTable(
                name: "VOVPartition");

            migrationBuilder.DropTable(
                name: "VOVDisk");
        }
    }
}
