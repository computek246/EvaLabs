using EvaLabs.Domain.Configurations.Base;
using EvaLabs.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvaLabs.Domain.Configurations
{
    public class TestBranchsConfiguration : ConfigurationBase<TestBranchs>
    {
        public override void ConfigureEntity(EntityTypeBuilder<TestBranchs> builder)
        {
            builder.HasOne(e => e.Branch)
                .WithMany(e => e.Tests)
                .HasForeignKey(e => e.BranchId);

            builder.HasOne(e => e.Test)
                .WithMany(e => e.Branchs)
                .HasForeignKey(e => e.TestId);
        }
    }
}
