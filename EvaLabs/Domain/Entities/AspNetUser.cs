using System;
using EvaLabs.Domain.Configurations;
using EvaLabs.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvaLabs.Domain.Entities
{
    public class AspNetUser : Auditable<int?>
    {
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string UserType { get; set; }
        public string UserPassword { get; set; }


        public string FullName => $"{FirstName} {LastName}".Trim();
    }

    public class AspNetUserConfiguration : ConfigurationBase<AspNetUser, int?>
    {
        public override void ConfigureEntity(EntityTypeBuilder<AspNetUser> builder)
        {
            builder.ToTable("AspNetUsers", "dbo");

            builder.Ignore(e => e.FullName);
            builder.Property(e => e.UserPassword).HasMaxLength(50);
            builder.Property(e => e.UserType).HasMaxLength(50);
            builder.HasIndex(e => e.NormalizedEmail).HasDatabaseName("EmailIndex");
            builder.HasIndex(e => e.NormalizedUserName).HasDatabaseName("UserNameIndex").IsUnique()
                .HasFilter("[NormalizedUserName] IS NOT NULL");
        }
    }
}