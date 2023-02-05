using Microsoft.Data.SqlClient;
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
    public class ProductItemVariationConfiguration : IEntityTypeConfiguration<ProductItemVariation>
    {
        public void Configure(EntityTypeBuilder<ProductItemVariation> builder)
        {
            builder.Property(x => x.ModifiedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);
            builder.HasOne(x => x.VariationOption).WithMany().HasForeignKey(x => x.VariationOptionId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
