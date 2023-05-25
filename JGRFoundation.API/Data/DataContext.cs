using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using JGRFoundation.Shared.Entities;

namespace JGRFoundation.API.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Appliance> HomeAppliances { get; set; }
        public DbSet<Battery> Batteries { get; set; }
        public DbSet<Category> Categories { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Appliance>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<Battery>().HasIndex(x => x.BatteryReference).IsUnique();
            modelBuilder.Entity<Category>().HasIndex(x => x.Name).IsUnique();
        }
    }
}
