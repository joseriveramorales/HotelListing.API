using Microsoft.EntityFrameworkCore;

namespace HoteListing.API.Data
{
    public class HotelListingDBContext : DbContext
    {
        public HotelListingDBContext(DbContextOptions options) : base(options)

        {

        }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Country> Countries { get; set; }


        /*
         * Run these two commands to update db using EF Core
         * 
         * Add-migration MigrationDescription
         * update-database
         */


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasData( 
                new Country
                {
                    Id = 1,
                    Name = "Jamaica",
                    ShortName = "JM"
                },
                new Country
                {
                    Id = 2,
                    Name = "Bahamas",
                    ShortName = "BS"
                },
                new Country
                {
                    Id = 3,
                    Name = "Cayman Island",
                    ShortName = "CI"
                },
                new Country
                {
                    Id = 4,
                    Name = "Puerto Rico",
                    ShortName = "PR"
                });

            modelBuilder.Entity<Hotel>().HasData(
                new Hotel
                {
                    Id = 1,
                    Name = "Sandals Resort and Spa",
                    Address = "Negril",
                    CountryId = 1,
                    Rating = 4.5
                },
                new Hotel
                {
                    Id = 2,
                    Name = "Comfort Suites",
                    Address = "George Town",
                    CountryId = 3,
                    Rating = 4.3
                },
                new Hotel
                {
                    Id = 3,
                    Name = "Grand Palladium",
                    Address = "Nassua",
                    CountryId = 2,
                    Rating = 5
                });
        }

    }
}
