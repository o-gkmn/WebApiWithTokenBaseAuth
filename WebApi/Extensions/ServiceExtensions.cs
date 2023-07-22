using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Contracts;
using Repositories.EFCore;
using Services;
using Services.Contracts;

namespace WebApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureSqlServer(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddDbContext<RepositoryContext>(options => 
                options.UseSqlServer(configuration.GetConnectionString("sqlConnection")));    
        }

        public static void ConfigureRepositoryManager(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }

        public static void ConfigureServiceManager(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
        }

        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthManager>();
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentity<User, IdentityRole>(opts =>
            {
                opts.Password.RequireDigit = true;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequiredLength = 6;

                opts.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<RepositoryContext>()
                .AddDefaultTokenProviders();
        }
    }
}
