using EvaLabs.Domain.Configurations.Base;
using EvaLabs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvaLabs.Domain.Configurations
{
    public class UserConfiguration : ConfigurationBase<User, int?>
    {
        public override void ConfigureEntity(EntityTypeBuilder<User> builder)
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
