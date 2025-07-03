using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Server; 
using TaskManager.Models;
using TaskManager.UI.Data;
using TaskManager.UI.Services;
using TaskManager.UI.Services.Interfaces;


var builder = WebApplication.CreateBuilder(args);

// Veritabaný baðlantýsý
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity (sadece kullanýcý bazlý kimlik doðrulama)
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{


    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<AppDbContext>();

// AuthenticationStateProvider
builder.Services.AddScoped<AuthenticationStateProvider,
    ServerAuthenticationStateProvider>();

// Blazor ve servisler
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddAuthorizationCore(); // yalnýzca 1 kez yeterli
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddSingleton<AppState>();
builder.Services.AddScoped<IUserService, UserService>();


var app = builder.Build();

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

    app.Run();
