using EvaLabs.Domain.Configurations.Base;
using EvaLabs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvaLabs.Domain.Configurations
{
    public class UserTestConfiguration : ConfigurationBase<UserTest>
    {
        public override void ConfigureEntity(EntityTypeBuilder<UserTest> builder)
        {
            builder.Property(e => e.TestDate).HasColumnType("date");
            builder.Property(e => e.ResultDate).HasColumnType("date");

            builder.HasOne(e => e.User)
                .WithMany(e => e.UserTests)
                .HasForeignKey(e => e.UserId);

            builder.HasOne(e => e.Lab)
                .WithMany(e => e.UserTests)
                .HasForeignKey(e => e.LabId);

            builder.HasOne(e => e.Branch)
                .WithMany(e => e.UserTests)
                .HasForeignKey(e => e.BranchId);

            builder.HasOne(e => e.Test)
                .WithMany(e => e.UserTests)
                .HasForeignKey(e => e.TestId);

            builder.HasOne(e => e.TestStatus)
                .WithMany(e => e.UserTests)
                .HasForeignKey(e => e.TestStatusId);

            builder.HasOne(e => e.City)
                .WithMany(e => e.UserTests)
                .HasForeignKey(e => e.CityId);

            builder.HasOne(e => e.Area)
                .WithMany(e => e.UserTests)
                .HasForeignKey(e => e.AreaId);
        }
    }
}