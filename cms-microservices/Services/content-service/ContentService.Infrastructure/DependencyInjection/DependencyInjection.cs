using ContentService.Application.Interfaces;
using ContentService.Infrastructure.Persistence;
using ContentService.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;


namespace ContentService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ContentDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IContentService, Application.Services.ContentService>();

        services.AddHttpClient<IUserServiceClient, UserServiceClient>(client =>
        {
            var userServiceUrl = configuration["Services:UserService"];
            if (string.IsNullOrEmpty(userServiceUrl))
            {
                throw new InvalidOperationException("UserService URL is not configured.");
            }
            client.BaseAddress = new Uri(userServiceUrl);
        });

        return services;
    }
}
