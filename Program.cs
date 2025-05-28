using Microsoft.EntityFrameworkCore;
using RedSismica.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);

// Configura EF Core con SQLite
builder.Services.AddDbContext<RedSismicaContext>(options =>
    options.UseSqlite("Data Source=RedSismica.db")
           .EnableSensitiveDataLogging()); // Habilitar registro de datos sensibles;

builder.Services.AddControllersWithViews();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<RedSismicaContext>();
    DbInitializer.Initialize(context);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Redirigir la raíz a la acción SeleccionarAccion
app.MapGet("/", context =>
{
    context.Response.Redirect("/OrdenInspeccion/SeleccionarAccion");
    return Task.CompletedTask;
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=OrdenInspeccion}/{action=SeleccionarAccion}/{id?}");

app.Run();