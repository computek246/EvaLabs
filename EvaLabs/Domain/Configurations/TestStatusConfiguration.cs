using EvaLabs.Domain.Configurations.Base;
using EvaLabs.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvaLabs.Domain.Configurations
{
    public class TestStatusConfiguration : ConfigurationBase<TestStatus>
    {
        public override void ConfigureEntity(EntityTypeBuilder<TestStatus> builder)
        {
        }
    }
}
