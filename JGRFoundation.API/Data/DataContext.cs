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
        public DbSet<Investor> Investors { get; set; }
        public DbSet<Panel> Panels { get; set; }

        public DbSet<Category> Categories { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Investor>().HasIndex(x => x.InvestorReference).IsUnique();
            modelBuilder.Entity<Panel>().HasIndex(x => x.PanelReference).IsUnique();

            modelBuilder.Entity<Category>().HasIndex(x => x.Name).IsUnique();
        }
    }
}
