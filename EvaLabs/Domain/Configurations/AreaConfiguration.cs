using EvaLabs.Domain.Configurations.Base;
using EvaLabs.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvaLabs.Domain.Configurations
{
    public class AreaConfiguration : ConfigurationBase<Area>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Area> builder)
        {
            builder.HasOne(e => e.City)
                .WithMany(e => e.Areas)
                .HasForeignKey(e => e.CityId);
        }
    }
}