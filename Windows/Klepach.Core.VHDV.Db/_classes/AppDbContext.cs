using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klepach.Core.VHDV.Db
{
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// <para>
        /// Override this method to configure the database (and other options) to be used for this context.
        /// This method is called for each instance of the context that is created.
        /// The base implementation does nothing.
        /// </para>
        /// <para>
        /// In situations where an instance of <see cref="T:Microsoft.EntityFrameworkCore.DbContextOptions" /> may or may not have been passed
        /// to the constructor, you can use <see cref="P:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.IsConfigured" /> to determine if
        /// the options have already been set, and skip some or all of the logic in
        /// <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" />.
        /// </para>
        /// </summary>
        /// <param name="optionsBuilder">A builder used to create or modify options for this context. Databases (and other extensions)
        /// typically define extension methods on this object that allow you to configure the context.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connString = System.Configuration.ConfigurationManager.AppSettings["DBConnection"];
            if (string.IsNullOrEmpty(connString))
                connString = @"Data Source=C:\_Enwicklung\_GitHub\Volume-Offline-Viewer\Database\vovData.db";

            optionsBuilder.UseSqlite(connString);
            //  .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);

            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<VOVFileSystemItem> FileSystemItems { get; set; }
        public DbSet<VOVPartition> Partitions { get; set; }
        public DbSet<VOVDisk> Disks { get; set; }
    }
}
