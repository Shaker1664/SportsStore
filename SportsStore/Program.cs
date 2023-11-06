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

var app = builder.Build();

app.UseDeveloperExceptionPage();
app.UseStatusCodePages();
app.UseStaticFiles();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute("pagination",
        "Products/Page{productPage}",
        new { Controller="Home", Action = "Index"});
    endpoints.MapDefaultControllerRoute();
});

SeedData.EnsurePopulated(app);

app.Run();
