using Microsoft.EntityFrameworkCore;

namespace PrestaToSap.model.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderRow> OrderRows { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(o => o.Id);
            entity.Ignore(o => o.prestashop);
            entity.Ignore(o => o.Associations);

            entity.HasMany(o => o.OrderRows)
                  .WithOne(or => or.Order)
                  .HasForeignKey(or => or.OrderId)
                  .OnDelete(DeleteBehavior.Cascade);
            
            // Configure decimal properties with precision and scale
            entity.Property(o => o.CarrierTaxRate).HasPrecision(18, 4);
            entity.Property(o => o.ConversionRate).HasPrecision(18, 4);
            entity.Property(o => o.TotalDiscounts).HasPrecision(18, 2);
            entity.Property(o => o.TotalDiscountsTaxExcl).HasPrecision(18, 2);
            entity.Property(o => o.TotalDiscountsTaxIncl).HasPrecision(18, 2);
            entity.Property(o => o.TotalPaid).HasPrecision(18, 2);
            entity.Property(o => o.TotalPaidReal).HasPrecision(18, 2);
            entity.Property(o => o.TotalPaidTaxExcl).HasPrecision(18, 2);
            entity.Property(o => o.TotalPaidTaxIncl).HasPrecision(18, 2);
            entity.Property(o => o.TotalProducts).HasPrecision(18, 2);
            entity.Property(o => o.TotalProductsWt).HasPrecision(18, 2);
            entity.Property(o => o.TotalShipping).HasPrecision(18, 2);
            entity.Property(o => o.TotalShippingTaxExcl).HasPrecision(18, 2);
            entity.Property(o => o.TotalShippingTaxIncl).HasPrecision(18, 2);
            entity.Property(o => o.TotalWrapping).HasPrecision(18, 2);
            entity.Property(o => o.TotalWrappingTaxExcl).HasPrecision(18, 2);
            entity.Property(o => o.TotalWrappingTaxIncl).HasPrecision(18, 2);
        });

        modelBuilder.Entity<OrderRow>(entity =>
        {
            entity.HasKey(or => or.Id);
            entity.Ignore(or => or.Associations);
            
            // Configure decimal properties with precision and scale
            entity.Property(or => or.ProductPrice).HasPrecision(18, 2);
            entity.Property(or => or.UnitPriceTaxExcl).HasPrecision(18, 2);
            entity.Property(or => or.UnitPriceTaxIncl).HasPrecision(18, 2);
        });

        // Ensure these types are not considered entities
        modelBuilder.Ignore<Prestashop>();
        modelBuilder.Ignore<Associations>();
    }
}