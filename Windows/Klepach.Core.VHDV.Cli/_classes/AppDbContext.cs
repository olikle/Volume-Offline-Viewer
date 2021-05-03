using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klepach.Core.VHDV.Cli
{
    class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connString = System.Configuration.ConfigurationManager.AppSettings["DBConnection"];
            if (string.IsNullOrEmpty(connString))
                connString = @"Data Source=C:\_Enwicklung\_GitHub\Volume-Offline-Viewer\Windows\Klepach.Core.VHDV.Cli\vovData.db";

            optionsBuilder.UseSqlite(connString);
            //  .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);

            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<VOVFile> vOVFiles { get; set; }
    }
}
