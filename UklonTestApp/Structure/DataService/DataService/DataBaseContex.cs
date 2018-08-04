using Microsoft.EntityFrameworkCore;
using UklonTestApp.Models;

namespace UklonTestApp.Structure.DataService.DataService
{
    public class DatabaseContext : DbContext
    {
        public DbSet<RegionTrafficStatusModel> RegionTrafficStatuses { get; set; }
        public DbSet<RegionModel> Regions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=MyDatabase.db");
        }
    }
}
