using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tello.Core.Entities;

namespace Tello.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x=>x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x=>x.Desc).IsRequired().HasMaxLength(200);
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(x => x.ModifiedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);
        }
    }
}
