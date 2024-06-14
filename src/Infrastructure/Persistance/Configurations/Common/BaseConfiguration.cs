using API.Domain.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.Persistance.Configurations.Common;

public class BaseConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseAuditableEntity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CreatedAt).HasColumnType("datetime2").IsRequired();
        builder.Property(x => x.ModifiedAt).HasColumnType("datetime2").IsRequired(false);
        builder.Property(x => x.CreatedBy).IsRequired();
        builder.Property(x => x.ModifiedBy).IsRequired(false);
        builder.Property(x => x.IP).IsRequired();
        builder.Property(x => x.IsDeleted).HasDefaultValue(false).IsRequired();
        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
