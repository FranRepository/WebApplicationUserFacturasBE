
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using WebApplicationBE.Data;
using WebApplicationBE.Interfaces;
using WebApplicationBE.Models;
using WebApplicationBE.Repositories;
using WebApplicationBE.Services;
using Microsoft.Extensions.Configuration;
using WebApplicationBE;



var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration; // Aquí accedes a la configuración



// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlite(configuration.GetConnectionString("ApplicationDbContext"));
});
// Register repositories
builder.Services.AddScoped<IFacturaRepository, FacturaRepository>();
builder.Services.AddScoped<IPersonaRepository, PersonaRepository>();

// Registra la clase Ventas en el contenedor de servicios
builder.Services.AddScoped<Ventas>();
builder.Services.AddScoped<Directorio>();

builder.Services.AddScoped<FacturaRestServiceController>();
builder.Services.AddScoped<DirectorioRestServiceController>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Nombre de tu API", Version = "v1" });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
   
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.MigrateDataBase();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
// Habilita CORS
app.UseCors(options =>
{
    options.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.UseSwagger();

// Habilitar middleware para servir Swagger UI (HTML, JS, CSS, etc.)
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Nombre de tu API V1");
});


app.Run();
