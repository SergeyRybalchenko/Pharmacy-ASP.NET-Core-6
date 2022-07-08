using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pharmacy.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Pharmacy.Data;
using Pharmacy.Areas.Identity.Data;
using Pharmacy.Domain;
using Pharmacy.Service;
using Pharmacy.Service.Abstract;
using Pharmacy.Domain.Repositories.Abstract;
using Pharmacy.Domain.Repositories.EntityFramework;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ConnectionString") ?? throw new InvalidOperationException("Connection string 'IdentityDBContextConnection' not found.");

builder.Services.AddDbContext<IdentityDBContext>(options =>
    options.UseSqlServer(connectionString));;

builder.Services.AddDefaultIdentity<PharmacyUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<IdentityDBContext>()
    ;;


// Add services to the container.
builder.Services.AddControllersWithViews();
    
builder.Services.AddDbContext<ApplicationDBContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));

builder.Services.AddTransient<IPagerService, PagerService>();
builder.Services.AddTransient<IProducts, EFProduct>();
builder.Services.AddTransient<DataManager>();
builder.Services.AddTransient<IProductService, ProductService>();

builder.Services.AddHttpContextAccessor();

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
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
