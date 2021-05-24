using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klepach.Core.VHDV.Db
{
    /// <summary>
    /// AppDbContext
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public class AppDbContext : DbContext
    {
        #region enum
        /// <summary>
        /// Database Types
        /// </summary>
        public enum VhdvDbType
        {
            SQLite
        }
        #endregion

        #region variable
        private string _databasePath = "";
        private VhdvDbType _dbType;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="AppDbContext"/> class.
        /// </summary>
        /// <param name="databasePath">The database path.</param>
        /// <param name="dbType">Type of the database.</param>
        public AppDbContext(string databasePath, VhdvDbType dbType)
        {
            _databasePath = databasePath;
            _dbType = dbType;
        }
        #endregion

        #region OnConfiguring
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
            //var connString = System.Configuration.ConfigurationManager.AppSettings["DBConnection"];
            var connString = "";
            if (_dbType == VhdvDbType.SQLite)
            {
                connString = @"Data Source=" + _databasePath;
                if (string.IsNullOrEmpty(connString))
                    connString = @"Data Source=C:\_Enwicklung\_GitHub\Volume-Offline-Viewer\Database\vhdvData.db";

                optionsBuilder.UseSqlite(connString);
                //  .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
            }
            base.OnConfiguring(optionsBuilder);
        }
        #endregion

        #region DBSets
        /// <summary>
        /// Gets or sets the file system items.
        /// </summary>
        /// <value>
        /// The file system items.
        /// </value>
        public DbSet<VHDVFileSystemItem> FileSystemItems { get; set; }
        /// <summary>
        /// Gets or sets the partitions.
        /// </summary>
        /// <value>
        /// The partitions.
        /// </value>
        public DbSet<VHDVPartition> Partitions { get; set; }
        /// <summary>
        /// Gets or sets the disks.
        /// </summary>
        /// <value>
        /// The disks.
        /// </value>
        public DbSet<VHDVDisk> Disks { get; set; }
        #endregion

    }
}
