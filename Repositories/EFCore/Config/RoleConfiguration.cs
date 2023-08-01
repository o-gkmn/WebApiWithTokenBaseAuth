using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.EFCore.Config
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(
                new Role
                {
                    Name = "User",
                    NormalizedName = "USER"
                },
                new Role
                {
                    Name = "Editor",
                    NormalizedName = "EDITOR"
                },
                new Role
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                }
            );
        }
    }
}
