using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace YarpGateway.API.Extensions
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddGatewaySwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "YARP Gateway",
                    Version = "v1",
                    Description = "API Gateway for microservices"
                });
            });

            return services;
        }
    }
}
