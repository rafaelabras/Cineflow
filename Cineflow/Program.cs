using Cineflow.extensions;
using Cineflow.helpers;
using Cineflow.@interface;
using Cineflow.@interface.CinemaInterfaces;
using Cineflow.@interface.IClienteRepository;
using Cineflow.repository;
using Cineflow.services;
using Cineflow.utils;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSingleton(connectionString);
builder.Services.AddScoped<DatabaseService>();
builder.Services.AddScoped<BCryptHelper>();
builder.Services.AddScoped<AESCryptoHelper>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IFilmeService, FilmeService>();
builder.Services.AddScoped<IFilmeRepository, FilmeRepositoy>();
builder.Services.AddScoped<ISalaService, SalaService>();
builder.Services.AddScoped<ISalaRepository, SalaRepository>();


builder.AddBuilderExtensions();
builder.WebHost.UseUrls("http://localhost:5039");

var app = builder.Build();
app.UseArchitectures();


app.Run();
