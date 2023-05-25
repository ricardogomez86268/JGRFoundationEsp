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
        public DbSet<Investor> Investors { get; set; }
        public DbSet<Panel> Panels { get; set; }
        public DbSet<Home> Homes { get; set; }
        public DbSet<TemporalHome> TemporalHomes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Appliance>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<Battery>().HasIndex(x => x.BatteryReference).IsUnique();
            modelBuilder.Entity<Home>().HasIndex(x => x.Id).IsUnique();
            modelBuilder.Entity<Investor>().HasIndex(x => x.InvestorReference).IsUnique();
            modelBuilder.Entity<Panel>().HasIndex(x => x.PanelReference).IsUnique();
        }
    }
}
