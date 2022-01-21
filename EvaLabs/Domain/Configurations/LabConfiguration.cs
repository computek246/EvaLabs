using EvaLabs.Domain.Configurations.Base;
using EvaLabs.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvaLabs.Domain.Configurations
{
    public class LabConfiguration : ConfigurationBase<Lab>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Lab> builder)
        {
        }
    }
}