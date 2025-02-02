using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;

namespace CourseSales.Discount.Api.Features.Discounts
{
    public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.ToCollection("discounts");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Code).HasElementName("code").HasMaxLength(10);
            builder.Property(x => x.Rate).HasElementName("rate");
            builder.Property(x => x.Created).HasElementName("created");
            builder.Property(x => x.Expired).HasElementName("expired");
            builder.Property(x => x.UserId).HasElementName("user_id");
            
            
        }
    }
}
