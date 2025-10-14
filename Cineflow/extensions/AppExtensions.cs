namespace Cineflow.extensions
{
    public static class AppExtensions
    {
        public static void UseArchitectures(this WebApplication app) {
            app.MapGet("/", () => "Hello World!");
        }        
    }
}

