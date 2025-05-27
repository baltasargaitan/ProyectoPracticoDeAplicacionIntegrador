using Microsoft.EntityFrameworkCore;
using RedSismica.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Configura EF Core con SQLite
builder.Services.AddDbContext<RedSismicaContext>(options =>
    options.UseSqlite("Data Source=RedSismica.db"));

builder.Services.AddControllersWithViews();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<RedSismicaContext>();
    DbInitializer.Initialize(context);
}

// Inicialización de datos opcional
// using (var scope = app.Services.CreateScope())
// {
//     var context = scope.ServiceProvider.GetRequiredService<RedSismicaContext>();
//     DbInitializer.Initialize(context); // Si lo agregás
// }

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=OrdenInspeccion}/{action=Index}/{id?}");

app.Run();
