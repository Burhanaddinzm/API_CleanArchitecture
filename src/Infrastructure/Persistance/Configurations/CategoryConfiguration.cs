using API.Domain.Entities;
using API.Infrastructure.Persistance.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Infrastructure.Persistance.Configurations;

public class CategoryConfiguration : BaseConfiguration<Category>
{
    public override void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(x => x.Name)
               .HasMaxLength(100)
               .HasColumnType("nvarchar")
               .IsRequired();

        builder.Property(x => x.Logo)
               .IsRequired();

        builder.HasOne(x => x.Parent)
               .WithMany(x => x.SubCategories)
               .HasForeignKey(x => x.ParentId);

        base.Configure(builder);
    }
}
