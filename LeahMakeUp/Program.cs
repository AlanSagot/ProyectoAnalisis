using DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Configuration;


var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AuthDbContextConnection") ?? throw new InvalidOperationException("Connection string 'AuthDbContextConnection' not found.");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<LeahDBContext>(options => options.UseSqlServer("name=ConnRSDB").LogTo(Console.WriteLine, LogLevel.Information));

//*** Identity*******
builder.Services.AddDbContext<AuthDbContext>(Options => Options.UseSqlServer(connectionString));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddDefaultUI();

builder.Services.AddRazorPages();
//*** Identity*******

var app = builder.Build();

/*public void ConfigureServices(IServiceCollection services)
{
    services.Configure<SmtpSettings>(Configuration.GetSection("SmtpSettings"));
    services.AddTransient<IEmailSender, SmtpEmailSender>();

    // Otras configuraciones de servicios
}*/


// Configure the HTTP request pipeline.
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

//*** Identity*******
app.MapRazorPages();

app.Run();
