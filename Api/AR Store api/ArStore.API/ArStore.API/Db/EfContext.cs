using ArStore.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArStore.API.Db
{
    public class EfContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Order> Orders { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                optionsBuilder.UseSqlServer(
                    "Data Source=localhost;Database=arstoreb_api;user= arstoreb_admin; password=Azaza7788");
//                optionsBuilder.UseSqlServer(
//                    "Data Source=DESKTOP-GE4L5QE;Database=ArStore;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }
    }
}