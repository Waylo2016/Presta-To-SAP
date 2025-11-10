using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace PrestaToSap.model.Context;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        // Build configuration similar to Program.cs
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var connStr = config.GetConnectionString("Default")
                     ?? config["ConnectionString"]
                     ?? Environment.GetEnvironmentVariable("ConnectionStrings__Default")
                     ?? "Server=localhost,1430;Database=PrestaToSap;User Id=sa;Password=UHJlc3RhVG9TYXAxIQ;TrustServerCertificate=True;Encrypt=False;";

        optionsBuilder.UseSqlServer(connStr);
        return new AppDbContext(optionsBuilder.Options);
    }
}