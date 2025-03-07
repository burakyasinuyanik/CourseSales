using CourseSales.Order.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSales.Order.Persistence.Configurations
{
    public class OrdercConfiguration : IEntityTypeConfiguration<Domain.Entities.Order>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Order> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.Id).ValueGeneratedNever();
            builder.Property(x => x.Code).IsRequired().HasMaxLength(10);
            builder.Property(x => x.BuyerId).IsRequired();
            builder.Property(x => x.Created).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.AdressId).IsRequired();
            builder.Property(x => x.TotalPrice).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(x => x.DiscountRate).HasColumnType("float");
            
            builder.HasMany(x=>x.OrderItems).WithOne(x => x.Order).HasForeignKey(x=>x.OrderId);
            builder.HasOne(x=>x.Adress).WithMany().HasForeignKey(x=>x.AdressId);

        }
    }
}
