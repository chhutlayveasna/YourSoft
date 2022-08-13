using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace YourSoft.DAL.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Name = "Manage Sample",
                    NormalizedName = "MANAGE SAMPLE"
                },
                new IdentityRole
                {
                    Name = "Manage User",
                    NormalizedName = "MANAGE USER"
                }
            );
        }
    }
}
