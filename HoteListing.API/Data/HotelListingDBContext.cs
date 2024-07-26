using HoteListing.API.Data.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HoteListing.API.Data
{
    public class HotelListingDBContext : IdentityDbContext<APIUser>
    {
        public HotelListingDBContext(DbContextOptions options) : base(options)

        {

        }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Country> Countries { get; set; }


        /*
         * Run these two commands to update db using EF Core, use the Package Manager Console
         * 
         * Add-migration MigrationDescription
         * update-database
         * 
         * Here Im using IEntityTypeConfiguration classes to simplify seed data for my db.
         * each class handles adding default values to my intial DB
         * each call to applyconfigurations uses the data. 
         */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new CountryConfiguration());
            modelBuilder.ApplyConfiguration(new HotelConfiguration());
        }
    }
}
