using InstaClone.Application.Interfaces;
using InstaClone.Application.Mappings;
using InstaClone.Application.Services;
using InstaClone.Application.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(typeof(UserMappings).Assembly);
            });
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.Configure<FrontendOptions>(
                 configuration.GetSection("FrontendBaseUrl"));

            return services;
        }
    }
}
