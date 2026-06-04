using InstaClone.Application.Interfaces;
using InstaClone.Domain.Interfaces;
using InstaClone.Infrastructure.Services;
using InstaClone.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.Configure<EmailSenderSettings>(configuration.GetSection(EmailSenderSettings.SectionName));
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            var bundledSettings = new ServedAppSettings
            {
                DefaultConnectionString = configuration.GetSection("ConnectionStrings:DefaultConnection").Value ?? string.Empty,
                FrontendBaseUrl = configuration.GetSection("FrontendBaseUrl").Value ?? string.Empty,
                JWTIssuer = configuration.GetSection("JWT:Issuer").Value ?? string.Empty,
                JWTAudience = configuration.GetSection("JWT:Audience").Value ?? string.Empty,
                JWTSecret = configuration.GetSection("JWT:Secret").Value ?? string.Empty,
                EmailSenderApiKey = configuration.GetSection("EmailSender:ApiKey").Value ?? string.Empty
            };

            services.AddSingleton<IServedAppSettings>(bundledSettings);

            return services;
        }
    }
}
