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
        });

        modelBuilder.Entity<OrderRow>(entity =>
        {
            entity.HasKey(or => or.Id);
            entity.Ignore(or => or.Associations);
        });

        // Ensure these types are not considered entities
        modelBuilder.Ignore<Prestashop>();
        modelBuilder.Ignore<Associations>();
    }
}