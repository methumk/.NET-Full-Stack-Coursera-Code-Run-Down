using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CoreWebApp.Data;
var builder = WebApplication.CreateBuilder(args);

// 1.? Dependency injection
// ? We are injecting our app context (by connection string) into the sql server and using that as a db context 
// Connection string we are INJECTING in the DBcontext in the server
// NOTE: connection string "CoreWEbAppContext" is specified in ./appsettings.json
builder.Services.AddDbContext<CoreWebAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CoreWebAppContext") ?? throw new InvalidOperationException("Connection string 'CoreWebAppContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
