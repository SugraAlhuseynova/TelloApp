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
    public class ProductOrderConfiguration : IEntityTypeConfiguration<ProductOrder>
    {
        public void Configure(EntityTypeBuilder<ProductOrder> builder)
        {
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(x => x.ModifiedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);

            builder.HasOne(p => p.ProductItem)
           .WithMany(pi => pi.ProductOrders)
           .HasForeignKey(p => p.ProductItemId);

            builder.Property(p => p.Price)
                //.HasComputedColumnSql("SELECT [pi].[SalePrice]*[po].[Count] from [ProductItems] [pi] join [ProductOrders] [po] on [po].[ProductItemId] = [pi].[Id]", stored: true)
                .HasComputedColumnSql($"{nameof(ProductOrder.Count)}*{nameof(ProductOrder.ProductItem.SalePrice)}", stored: true)
                //.HasColumnType("decimal(18,2)")
                .IsRequired();
        }
    }
}
