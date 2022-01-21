using EvaLabs.Domain.Configurations.Base;
using EvaLabs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvaLabs.Domain.Configurations
{
    public class TestConfiguration : ConfigurationBase<Test>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Test> builder)
        {
            builder.Property(e => e.Price).HasColumnType("decimal(18, 6)");
        }
    }
}