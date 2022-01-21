using EvaLabs.Domain.Configurations.Base;
using EvaLabs.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvaLabs.Domain.Configurations
{


    public class TestResultConfiguration : ConfigurationBase<TestResult>
    {
        public override void ConfigureEntity(EntityTypeBuilder<TestResult> builder)
        {
            builder.HasOne(e => e.UserTest)
                .WithOne(e => e.TestResult)
                .HasForeignKey<TestResult>(e => e.UserTestId);
        }
    }
}
