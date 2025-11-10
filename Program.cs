using Microsoft.EntityFrameworkCore;
using PrestaToSap.Components;
using PrestaToSap.model.Context;

namespace PrestaToSap;

public class Program
{
    public static void Main(string[] args)
    {
        // Ensure SQLite native bits are initialized for design-time and runtime
        SQLitePCL.Batteries.Init();

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();
        
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite("Data Source=PrestaToSap.db"));
        
        // builder configurations setup
        builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();
        

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        if (app.Environment.IsDevelopment())
        {
            app.UseHttpsRedirection();
        }
        // In IIS without HTTPS binding, HTTPS redirection can cause issues; enable once HTTPS is configured.

        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}