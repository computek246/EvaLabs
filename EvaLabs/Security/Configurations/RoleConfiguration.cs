using System.Linq;
using EvaLabs.Helper.Constant;
using EvaLabs.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static System.String;

namespace EvaLabs.Security.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> entityBuilder)
        {
            var fields = typeof(AppValues.AppRoles).GetFields().ToList();
            var roles = fields
                .Select(e =>
                {
                    var id = fields.IndexOf(e) + 1;
                    var name = e.GetValue(e.Name)?.ToString() ?? "";
                    return new Role
                    {
                        Id = id,
                        Name = name,
                        NormalizedName = name.ToUpperInvariant(),
                    };
                });

            entityBuilder.HasData(roles.Where(e => !IsNullOrEmpty(e.Name)));

            // Each Role can have many entries in the UserRole join table
            entityBuilder.HasMany(e => e.UserRoles)
                .WithOne(e => e.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            // Each Role can have many associated RoleClaims
            entityBuilder.HasMany(e => e.RoleClaims)
                .WithOne(e => e.Role)
                .HasForeignKey(rc => rc.RoleId)
                .IsRequired();
        }
    }
}