using AlphtechPlugins.Data;
using AlphtechPlugins.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add Database Context with SQLite
builder.Services.AddDbContext<AlphtechDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=alphtech.db"));

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IPluginCatalogService, PluginCatalogService>();
builder.Services.AddSingleton<ICartService, CartService>();
builder.Services.AddSingleton<ILicenseService, LicenseService>();

var app = builder.Build();

// Ensure SQLite database is created and seeded with products on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AlphtechDbContext>();
    AlphtechDbContext.SeedData(db);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
