using Microsoft.EntityFrameworkCore;
using UklonTestApp.Models;

namespace UklonTestApp.Structure.DataService.DataService
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<RegionTrafficStatusModel> RegionTrafficStatuses { get; set; }
        public DbSet<RegionModel> Regions { get; set; }
    }
}
