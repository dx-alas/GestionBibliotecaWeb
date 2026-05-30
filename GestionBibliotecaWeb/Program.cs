using GestionBibliotecaWeb.Models;
using GestionBibliotecaWeb.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<CloudinaryService>();

builder.Services.AddSession();

var conString = builder.Configuration.GetConnectionString("GestionBiblioteca") ??
     throw new InvalidOperationException("Connection string 'GestionBiblioteca'" +
    " not found.");

builder.Services.AddDbContext<GestionBibliotecaContext>(options =>
    options.UseSqlServer(conString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();