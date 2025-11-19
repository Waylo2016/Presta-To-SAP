using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace PrestaToSap.model.Context;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        // Build configuration similar to Program.cs, strictly from appsettings.json
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        var connStr = config.GetConnectionString("Default");
        if (string.IsNullOrWhiteSpace(connStr))
        {
            throw new InvalidOperationException("Database connection string 'ConnectionStrings:Default' is missing in appsettings.json.");
        }

        optionsBuilder.UseSqlServer(connStr);
        return new AppDbContext(optionsBuilder.Options);
    }
}