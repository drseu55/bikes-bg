using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bikes_bg.Models;
using Microsoft.EntityFrameworkCore;

namespace bikes_bg.Repository.Base
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<BikeBrand> bikeBrands { get; set; }
    }

}
