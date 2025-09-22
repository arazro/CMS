using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UserService.Application.Interfaces;
using UserService.Application.Services;
using UserService.Infrastructure.Persistence;

namespace UserService.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUserServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<UserDbContext>(options =>
                options.UseNpgsql(connectionString));
            
            services.AddScoped<IUserService, Application.Services.UserService>();
            
            return services;
        }
    }
}
