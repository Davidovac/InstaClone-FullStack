using InstaClone.Application.Interfaces;
using InstaClone.Domain.Interfaces;
using InstaClone.Infrastructure.Repositories;
using InstaClone.Infrastructure.Services;
using InstaClone.Infrastructure.Settings;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
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
            services.AddScoped<ILocalImageStorageService, LocalImageStorageService>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<ILikeRepository, LikeRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();

            services.Configure<ConnectionOptions>(
                configuration.GetSection("ConnectionStrings"));
            services.AddTransient<IConnectionOptions>(provider =>
            provider.GetRequiredService<IOptions<ConnectionOptions>>().Value);

            services.Configure<JwtOptions>(
                configuration.GetSection("JwtSettings"));
            services.AddTransient<IJwtOptions>(provider =>
            provider.GetRequiredService<IOptions<JwtOptions>>().Value);

            services.Configure<EmailSenderOptions>(
                configuration.GetSection("EmailSenderSettings"));
            services.AddTransient<IEmailSenderOptions>(provider =>
            provider.GetRequiredService<IOptions<EmailSenderOptions>>().Value);

            return services;
        }
    }
}
