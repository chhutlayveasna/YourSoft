using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using YourSoft.DAL.Configurations;

namespace YourSoft.DAL.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApiUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Sample> Samples { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.Entity<Sample>().HasData(
                new Sample
                {
                    Id = 1,
                    Name = "Sample Data 1",
                    Date = DateTime.Now,
                    IsActive = true,
                },
                new Sample
                {
                    Id = 2,
                    Name = "Sample Data 2",
                    Date = DateTime.Now,
                    IsActive = true,
                },
                new Sample
                {
                    Id = 3,
                    Name = "Sample Data 2",
                    Date = DateTime.Now,
                    IsActive = true,
                }
            );
        }
    }
}