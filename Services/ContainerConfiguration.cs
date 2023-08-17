using Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Contracts;



namespace Services
{
    public static class ContainerConfiguration
    {
        const string REFRESH_TOKEN_OPTIONS_CONF_SECTION = "Auth:RefreshToken";
        public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RefreshTokenOptions>(configuration.GetSection(REFRESH_TOKEN_OPTIONS_CONF_SECTION));

            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IHasherService, HasherService>();
            services.AddTransient<IUserService, UserService>();

            return services;
        }
    }
}
