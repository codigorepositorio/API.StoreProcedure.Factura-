using CANVIA.RETO.Factura.Repository;
using CANVIA.RETO.Factura.Services;
using LoggerService;
using LoggerServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CANVIA.RETO.Factura.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            //Cliente Service
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<ClienteService>();

            //ItemDetalle Service
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<ItemService>();

            //Factura Service
            services.AddScoped<IFacturaRepository, FacturaRepository>();
            services.AddScoped<FacturaService>();

            //FacturaConsulta Service
            services.AddScoped<IFacturaConsultaRepository, FacturaConsultaRepository>();
            
        }
        public static IApplicationBuilder ConfigureCors(this IApplicationBuilder app)
        {
            app.UseCors(builder =>
             builder.WithOrigins("http://localhost:4200", "http://localhost:4201")
             .AllowAnyHeader()
              .AllowAnyMethod());
            return app;
        }


        public static void ConfigureLoggerService(this IServiceCollection services) =>
        services.AddScoped<ILoggerManager, LoggerManager>();
    }
}
