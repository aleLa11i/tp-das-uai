using Application.Abstractions;
using Application.Commands;
using Application.Strategies;
using Domain.Repositories;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Presentation;

var builder = Host.CreateApplicationBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Information);

// 1. Obtiene la ruta de ejecución actual de la app (ej: bin/Debug/net9.0/)
var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

// 2. Combina las rutas creando el archivo de forma automática allí dentro
var connectionString = $"Data Source={Path.Combine(baseDirectory, "Pintor.db")}";


// 2. Registrar el DbContext en el contenedor de dependencias usando el proveedor de SQLite
builder.Services.AddDbContext<MainDbContext>(options =>
{
    options.UseSqlite(connectionString);
    //Deshabilitar logs en consola
    options.ConfigureWarnings(warnings =>
        warnings.Ignore(RelationalEventId.CommandExecuted));
});

builder.Services.AddScoped<IPintorRepository, PintorRepository>();
builder.Services.AddScoped<ICreatePintorHandler, CreatePintorHandler>();
builder.Services.AddScoped<IUpdatePintorHandler, UpdatePintorHandler>();
builder.Services.AddScoped<IGetPintoresHandler, GetPintoresHandler>();
builder.Services.AddScoped<IGetPintorByIdHandler, GetPintorByIdHandler>();
builder.Services.AddScoped<IDeletePintorHandler, DeletePintorHandler>();
//Patron Strategy:
builder.Services.AddScoped<IOrdenamientoPintorStrategy, OrdenPorIdStrategy>();
builder.Services.AddScoped<IOrdenamientoPintorStrategy, OrdenPorNombreStrategy>();
builder.Services.AddScoped<IOrdenamientoPintorStrategy, OrdenPorApellidoStrategy>();
builder.Services.AddTransient<MenuPrincipal>();

using var host = builder.Build();

var menu = host.Services.GetRequiredService<MenuPrincipal>();
await menu.EjecutarAsync();