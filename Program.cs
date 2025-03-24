<<<<<<< HEAD
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

          

            // Add the session services
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Set the session timeout
                options.Cookie.HttpOnly = true; // Set the cookie to be HTTP only
                options.Cookie.IsEssential = true; // Set the cookie to be essential
            });

            // Build the app
            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();
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
=======
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add session services to the container
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Customize as needed
    options.Cookie.HttpOnly = true; // Make sure cookies are only accessible via HTTP
    options.Cookie.IsEssential = true; // Ensure cookies are essential for sessions
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Ensure HSTS is used in production for HTTPS
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Add session middleware to the pipeline
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
>>>>>>> 4fc9e1d (Upload Minesweeper project with database connection)
