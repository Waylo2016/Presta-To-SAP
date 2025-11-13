using Microsoft.EntityFrameworkCore;
using PrestaToSap.API;
using PrestaToSap.Components;
using PrestaToSap.model.Context;
using PrestaToSap.services;

namespace PrestaToSap;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

           
        // Configuration: read only from appsettings.json files (no environment variables)
        builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

        // Get connection string strictly from appsettings.json (ConnectionStrings:Default)
        var connectionString = builder.Configuration.GetConnectionString("Default");
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("Database connection string 'ConnectionStrings:Default' is missing in appsettings.json.");
        }
        
        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

        builder.Services.AddHttpClient();
        
        // Add scoped services
        builder.Services.AddScoped<PrestaApiService>();
        builder.Services.AddScoped<PrestaOrderIngestionService>();
        

        var app = builder.Build();

        // Apply database schema: wait for DB and use migrations when present; otherwise create schema
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            const int maxAttempts = 10;
            var delay = TimeSpan.FromSeconds(3);
            for (var attempt = 1; attempt <= maxAttempts; attempt++)
            {
                try
                {
                    if (!db.Database.GetMigrations().Any())
                    {
                        db.Database.EnsureCreated();
                    }
                    else
                    {
                        db.Database.Migrate();
                    }
                    break;
                }
                catch (Exception) when (attempt < maxAttempts)
                {
                    Thread.Sleep(delay);
                }
            }
        }

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        

        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}