using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PrestaToSap.model.Context;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        // Ensure SQLite native components are available for EF tooling
        SQLitePCL.Batteries.Init();

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlite("Data Source=PrestaToSap.db");
        return new AppDbContext(optionsBuilder.Options);
    }
}