using CineFlow.Endpoints;

namespace Cineflow.extensions
{
    public static class AppExtensions
    {
        public static void UseArchitectures(this WebApplication app) {
            app.MapClienteEndpoints();
            app.MapFilmeEndpoints();
            app.MapSalaEndpoint();
        }        
    }
}

