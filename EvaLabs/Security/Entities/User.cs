using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvaLabs.Security.Entities
{
    public class User : IdentityUser<int>
    {
        public User()
        {
            Claims = new HashSet<UserClaim>();
            Logins = new HashSet<UserLogin>();
            Tokens = new HashSet<UserToken>();
            UserRoles = new HashSet<UserRole>();
        }


        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string UserType { get; set; }
        public string UserPassword { get; set; }


        public int? CreatorId { get; set; }
        public int? ModifierId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public string FullName => $"{FirstName} {LastName}".Trim();


        public ICollection<UserClaim> Claims { get; set; }
        public ICollection<UserLogin> Logins { get; set; }
        public ICollection<UserToken> Tokens { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }


    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entityBuilder)
        {
            entityBuilder.ToTable("AspNetUsers", "dbo", builder => builder.ExcludeFromMigrations());

            // Each User can have many UserClaims
            entityBuilder.HasMany(e => e.Claims)
                .WithOne(e => e.User)
                .HasForeignKey(uc => uc.UserId)
                .IsRequired();

            // Each User can have many UserLogins
            entityBuilder.HasMany(e => e.Logins)
                .WithOne(e => e.User)
                .HasForeignKey(ul => ul.UserId)
                .IsRequired();

            // Each User can have many UserTokens
            entityBuilder.HasMany(e => e.Tokens)
                .WithOne(e => e.User)
                .HasForeignKey(ut => ut.UserId)
                .IsRequired();

            // Each User can have many entries in the UserRole join table
            entityBuilder.HasMany(e => e.UserRoles)
                .WithOne(e => e.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
        }
    }
}