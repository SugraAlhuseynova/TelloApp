using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Tello.Core.Entities;

namespace Tello.Data.Configurations
{
    internal class SlideConfiguration : IEntityTypeConfiguration<Slide>
    {
        public void Configure(EntityTypeBuilder<Slide> builder)
        {
            builder.Property(x => x.Title).IsRequired().HasMaxLength(40);
            builder.Property(x => x.Desc).IsRequired().HasMaxLength(150);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(x => x.ModifiedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(x => x.BackgroundPhotoStr).IsRequired().HasMaxLength(100);
            builder.Property(x => x.ProductPhotoStr).IsRequired().HasMaxLength(100);
            builder.Ignore(x => x.BackgroundPhoto);
            builder.Ignore(x => x.ProductPhoto);

        }
    }
}
