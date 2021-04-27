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
        }

        public DbSet<BikeBrand> bikeBrands { get; set; }
    }

}
