using Domain.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Contracts;
using Services.MappingConfig;

namespace Services
{
    public static class ContainerConfiguration
    {
        const string REFRESH_TOKEN_OPTIONS_CONF_SECTION = "Auth:RefreshToken";
        public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            CoffeeMapConfig.SetCoffeeMapping();

            services.Configure<RefreshTokenOptions>(configuration.GetSection(REFRESH_TOKEN_OPTIONS_CONF_SECTION));

            services.AddTransient<IFileService, FileService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICoffeeService, CoffeeService>();

            return services;
        }
    }
}
