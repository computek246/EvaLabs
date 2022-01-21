using EvaLabs.Domain.Configurations.Base;
using EvaLabs.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvaLabs.Domain.Configurations
{
    public class BranchConfiguration : ConfigurationBase<Branch>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Branch> builder)
        {
            builder.HasOne(e => e.Lab)
                .WithMany(e => e.Branches)
                .HasForeignKey(e => e.LabId);

            builder.HasOne(e => e.Area)
                .WithMany(e => e.Branches)
                .HasForeignKey(e => e.AreaId);
        }
    }
}