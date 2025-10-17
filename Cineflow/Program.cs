using Cineflow.extensions;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSingleton(connectionString);
builder.Services.AddScoped<DatabaseService>();

builder.AddBuilderExtensions();

var app = builder.Build();
app.UseArchitectures();


app.Run();
