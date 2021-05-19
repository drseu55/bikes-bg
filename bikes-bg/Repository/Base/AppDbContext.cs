using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bikes_bg.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace bikes_bg.Repository.Base
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("USERS");

            modelBuilder.Entity<BikeModel>()
                .HasOne(m => m.bikeBrand)
                .WithMany(b => b.bikeModels)
                .HasForeignKey(m => m.brandID);

            modelBuilder.Entity<City>()
                .HasOne(c => c.region)
                .WithMany(r => r.cities)
                .HasForeignKey(c => c.regionID);
    }

        public DbSet<BikeModel> bikeModels { get; set; }
        public DbSet<BikeBrand> bikeBrands { get; set; }
        public DbSet<BikeCategory> bikeCategories { get; set; }
        public DbSet<BikeEngineType> bikeEngineTypes { get; set; }
        public DbSet<BikeColor> bikeColors { get; set; }
    }

}
