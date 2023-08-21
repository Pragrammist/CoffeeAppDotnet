using EFCore.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EFCore
{
    public static class ContainerConfiguration
    {
        public static IServiceCollection RegisterDataLayer(this IServiceCollection services, IConfiguration configuration)
        {
            string connection = configuration.GetConnectionString("DefaultConnection") ?? 
                throw new NullReferenceException("connection string is null");

            
            services.AddDbContext<OverallDbContext>(opt =>
            {
                opt.UseSqlServer(connection);
            });

            services.AddScoped<IRepository, RepositoryImpl>();
            
            return services;
        }
    }
}