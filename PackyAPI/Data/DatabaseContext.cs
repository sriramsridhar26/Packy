using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PackyAPI.Configurations.Entities;

namespace PackyAPI.Data
{
    public class DatabaseContext : IdentityDbContext<ApiUser>
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Country> Countries { get; set; }   
        public DbSet<Hotel> Hotels { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.Entity<Country>().HasData(
                new Country
                {
                    Id = 1,
                    Name = "Quebec",
                    shortname = "QC"
                },
                new Country
                {
                    Id = 2,
                    Name = "Ontario",
                    shortname = "ON"
                },
                new Country
                {
                    Id = 3,
                    Name = "New Brunswick",
                    shortname = "NB"
                },
                new Country
                {
                    Id = 4,
                    Name = "British Columbia",
                    shortname = "BC"
                }
                );

            modelBuilder.Entity<Hotel>().HasData(
                new Hotel
                {
                    Id = 1,
                    Name = "The Quebec Hotel",
                    Address = "QCb",
                    CountryId = 1,
                    Rating = 4.5
                },
                new Hotel
                {
                    Id = 2,
                    Name = "The Ontario Hotel",
                    Address = "ONt",
                    CountryId = 2,
                    Rating = 4.2
                },
                new Hotel
                {
                    Id = 3,
                    Name = "The Brunswick Hotel",
                    Address = "NBs",
                    CountryId = 3,
                    Rating = 4.0
                },
                new Hotel
                {
                    Id = 4,
                    Name = "The Vancouver Hotel",
                    Address = "BCo",
                    CountryId = 4,
                    Rating = 4.9
                }
                );
        }


    }
}
