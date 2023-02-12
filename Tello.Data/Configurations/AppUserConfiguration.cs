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
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(x => x.Fullname).IsRequired(true).HasMaxLength(30);
            builder.Property(x => x.UserName).IsRequired(false);
            builder.Property(x=>x.Email).IsRequired(true).HasMaxLength(40);
            builder.Property(x => x.PhoneNumber).IsRequired(true).HasMaxLength(10).IsFixedLength();
        }
    }
}
