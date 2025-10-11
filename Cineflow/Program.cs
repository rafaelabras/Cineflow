using Cineflow.extensions;

var builder = WebApplication.CreateBuilder(args);
builder.AddBuilderExtensions();

var app = builder.Build();
app.UseArchitectures();

app.MapGet("/", () => "Hello World!");

app.Run();
