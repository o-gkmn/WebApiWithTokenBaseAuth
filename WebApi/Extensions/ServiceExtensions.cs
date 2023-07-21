using Repositories;
using Repositories.Contracts;
using Repositories.EFCore;
using Services;
using Services.Contracts;

namespace WebApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureRepositoryManager(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }

        public static void ConfigureServiceManager(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
        }

        public static void RegisterRepository(this IServiceCollection services)
        {
            services.AddSingleton<IAuthenticationRepository, AuthenticationRepository>();
        }

        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthManager>();
        }
    }
}
