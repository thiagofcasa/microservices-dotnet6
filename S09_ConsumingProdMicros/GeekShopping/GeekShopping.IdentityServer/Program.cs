using GeekShopping.IdentityServer.Configuration;
using GeekShopping.IdentityServer.Model;
using GeekShopping.IdentityServer.Model.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Configuration of database connection
var connection = builder.Configuration["MySQLConnection:MySQLConnection"];

builder.Services.AddDbContext<MySQLContext>(options => options.
        UseMySql(connection, new MySqlServerVersion(new Version(8, 0, 32))));

//Configuration of IdentityService
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().
            AddEntityFrameworkStores<MySQLContext>().
            AddDefaultTokenProviders();

builder.Services.AddIdentityServer(options =>
{
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseSuccessEvents = true;
    options.EmitStaticAudienceClaim = true; 
}).AddInMemoryIdentityResources(
        IdentityConfiguration.IdentityResources)
        .AddInMemoryApiScopes(IdentityConfiguration.ApiScopes)
        .AddInMemoryClients(IdentityConfiguration.Clients)
        .AddAspNetIdentity<ApplicationUser>()
        .AddDeveloperSigningCredential();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
