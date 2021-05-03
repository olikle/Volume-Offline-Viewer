﻿// <auto-generated />
using System;
using Klepach.Core.VHDV.Cli;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Klepach.Core.VHDV.Cli.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210503211536_AddDiskPartitionAndFile")]
    partial class AddDiskPartitionAndFile
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("Klepach.Core.VHDV.Cli.VOVDisk", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Caption")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirmwareRevision")
                        .HasColumnType("TEXT");

                    b.Property<string>("Manufacturer")
                        .HasColumnType("TEXT");

                    b.Property<string>("MediaType")
                        .HasColumnType("TEXT");

                    b.Property<string>("Model")
                        .HasColumnType("TEXT");

                    b.Property<string>("SerialNumber")
                        .HasColumnType("TEXT");

                    b.Property<long>("Size")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SystemName")
                        .HasColumnType("TEXT");

                    b.Property<long>("TotalCylinders")
                        .HasColumnType("INTEGER");

                    b.Property<long>("TotalHeads")
                        .HasColumnType("INTEGER");

                    b.Property<long>("TotalSectors")
                        .HasColumnType("INTEGER");

                    b.Property<long>("TotalTracks")
                        .HasColumnType("INTEGER");

                    b.Property<long>("TracksPerCylinder")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Type")
                        .HasColumnType("TEXT");

                    b.Property<string>("volumeId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("VOVDisk");
                });

            modelBuilder.Entity("Klepach.Core.VHDV.Cli.VOVFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("FileName")
                        .HasColumnType("TEXT");

                    b.Property<string>("FilePath")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LastModifiered")
                        .HasColumnType("TEXT");

                    b.Property<int?>("PartitionId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("Size")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PartitionId");

                    b.ToTable("vOVFiles");
                });

            modelBuilder.Entity("Klepach.Core.VHDV.Cli.VOVPartition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Caption")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("DeviceID")
                        .HasColumnType("TEXT");

                    b.Property<int?>("DiskId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DriveType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FileSystem")
                        .HasColumnType("TEXT");

                    b.Property<int>("MediaType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("SystemName")
                        .HasColumnType("TEXT");

                    b.Property<bool>("VolumeDirty")
                        .HasColumnType("INTEGER");

                    b.Property<string>("VolumeName")
                        .HasColumnType("TEXT");

                    b.Property<string>("VolumeSerialNumber")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DiskId");

                    b.ToTable("VOVPartition");
                });

            modelBuilder.Entity("Klepach.Core.VHDV.Cli.VOVFile", b =>
                {
                    b.HasOne("Klepach.Core.VHDV.Cli.VOVPartition", "Partition")
                        .WithMany("Files")
                        .HasForeignKey("PartitionId");

                    b.Navigation("Partition");
                });

            modelBuilder.Entity("Klepach.Core.VHDV.Cli.VOVPartition", b =>
                {
                    b.HasOne("Klepach.Core.VHDV.Cli.VOVDisk", "Disk")
                        .WithMany("Partitions")
                        .HasForeignKey("DiskId");

                    b.Navigation("Disk");
                });

            modelBuilder.Entity("Klepach.Core.VHDV.Cli.VOVDisk", b =>
                {
                    b.Navigation("Partitions");
                });

            modelBuilder.Entity("Klepach.Core.VHDV.Cli.VOVPartition", b =>
                {
                    b.Navigation("Files");
                });
#pragma warning restore 612, 618
        }
    }
}
