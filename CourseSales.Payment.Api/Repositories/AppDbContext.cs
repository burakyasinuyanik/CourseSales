using Microsoft.EntityFrameworkCore;

namespace CourseSales.Payment.Api.Repositories
{
    public class AppDbContext(DbContextOptions options) : DbContext(options)
    {

        public DbSet<Payment> Payments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).ValueGeneratedNever();
                entity.Property(p => p.UserId).IsRequired();
                entity.Property(p => p.OrderCode).IsRequired().HasMaxLength(10);
                entity.Property(p => p.Created).IsRequired();
                entity.Property(p => p.Amount).IsRequired().HasPrecision(18, 2);
                entity.Property(p => p.Status).IsRequired();


            });
            base.OnModelCreating(modelBuilder);
        }

    }
}
