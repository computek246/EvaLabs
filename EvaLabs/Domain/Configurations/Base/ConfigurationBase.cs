using EvaLabs.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvaLabs.Domain.Configurations.Base
{
    public abstract class ConfigurationBase<TEntity>
        : ConfigurationBase<TEntity, int>
        where TEntity : Auditable<int>
    {
    }


    public abstract class ConfigurationBase<TEntity, TForeignKey>
        : IEntityTypeConfiguration<TEntity>
        where TEntity : Auditable<TForeignKey>
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            ConfigureEntity(builder);

            builder.HasKey(e => e.Id);
            builder.HasQueryFilter(e => e.IsDeleted == false);

            builder.Property(e => e.CreatorId).HasDefaultValue(120);
            builder.Property(e => e.ModifierId).HasDefaultValue(120);
            builder.Property(e => e.CreationDate).HasColumnType("datetime").HasDefaultValueSql("getdate()");
            builder.Property(e => e.LastModifiedDate).HasColumnType("datetime").HasDefaultValueSql("getdate()");
        }

        public abstract void ConfigureEntity(EntityTypeBuilder<TEntity> builder);
    }
}