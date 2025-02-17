using CLCMinesweeperMilestone.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.IO; // For Directory.GetCurrentDirectory()

namespace CLCMinesweeperMilestone
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Create the builder
            var builder = WebApplication.CreateBuilder(args);

            // Manually build the configuration if you want to read from appsettings.json yourself.
            // Otherwise, builder.Configuration already loads appsettings.json by default.
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Register ApplicationDbContext with the connection string from appsettings.json
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Build the app
            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            // If MapStaticAssets is a custom extension, ensure you have the correct using or extension method.
            app.MapStaticAssets();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets(); // Also ensure this extension is available

            app.Run();
        }
    }
}
