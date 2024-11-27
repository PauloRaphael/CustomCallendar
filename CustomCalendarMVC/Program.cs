using DataRepository.Data;
using DataRepository.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<PasswordAuthorizeAttribute>();
});

builder.Services.AddScoped<PasswordAuthorizeAttribute>();

builder.Services.AddDbContext<CustomCalendarDBContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 23))));

// Register SeedingService
builder.Services.AddScoped<BlockService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<LoginService>();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Shared/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Blocks}/{action=Index}/{id?}");

app.Run();
