using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PrestaToSap.model.Context;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        var connStr = Environment.GetEnvironmentVariable("ConnectionStrings__Default")
                      ?? "Server=localhost,1433;Database=PrestaToSap;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True;Encrypt=False;";
        optionsBuilder.UseSqlServer(connStr);
        return new AppDbContext(optionsBuilder.Options);
    }
}