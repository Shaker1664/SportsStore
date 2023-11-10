using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsStore.Contracts;
using SportsStore.Extensions;
using SportsStore.Helpers;
using SportsStore.Implementations;
using SportsStore.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.ConfigureDatabase(builder.Configuration);
builder.Services.AddScoped<IStoreRepository, StoreRepository>();
builder.Services.AddRazorPages();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

var app = builder.Build();

app.UseDeveloperExceptionPage();
app.UseStatusCodePages();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute("catpage",
        "{category}/Page{productPage:int}",
        new { Controller = "Home", action = "Index" });

    endpoints.MapControllerRoute("page",
        "Page{productPage:int}",
        new { Controller = "Home", action = "Index", productPage = 1 });

    endpoints.MapControllerRoute("category", 
        "{category}",
        new { Controller = "Home", action = "Index", productPage = 1 });

    endpoints.MapControllerRoute("pagination",
        "Products/Page{productPage}",
        new { Controller="Home", action = "Index", productPage = 1 });

    endpoints.MapDefaultControllerRoute();
    endpoints.MapRazorPages();
});

SeedData.EnsurePopulated(app);

app.Run();
