using API.Domain.Entities;
using API.Infrastructure.Persistance.Configurations.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Infrastructure.Persistance.Configurations;

public class ProductConfiguration : BaseConfiguration<Product>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(x => x.Name)
               .HasMaxLength(150)
               .IsRequired();

        builder.Property(x => x.Description)
               .HasMaxLength(550)
               .IsRequired(false);

        builder.Property(x => x.Price)
               .IsRequired();

        builder.Property(x => x.DiscountPrice)
               .IsRequired();

        builder.HasOne(x => x.Category)
               .WithMany(x => x.Products)
               .HasForeignKey(x => x.CategoryId);

        base.Configure(builder);
    }
}
