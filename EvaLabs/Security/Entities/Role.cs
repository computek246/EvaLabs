using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvaLabs.Security.Entities
{
    public class Role : IdentityRole<int>
    {
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
            RoleClaims = new HashSet<RoleClaim>();
        }

        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<RoleClaim> RoleClaims { get; set; }
    }

    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> entityBuilder)
        {
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