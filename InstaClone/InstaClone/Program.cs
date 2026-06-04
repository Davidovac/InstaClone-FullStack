using AutoMapper;
using InstaClone.Api.Middleware;
using InstaClone.Application.Interfaces;
using InstaClone.Application.Mappings;
using InstaClone.Application.Services;
using InstaClone.Domain.Entities;
using InstaClone.Domain.Interfaces;
using InstaClone.Infrastructure;
using InstaClone.Infrastructure.Persistence;
using InstaClone.Infrastructure.Repositories;
using InstaClone.Infrastructure.Services;
using InstaClone.Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.AspNetCore;
using Serilog.Settings.Configuration;
using System.IO;
using System.Text;



try
{

    var builder = WebApplication.CreateBuilder(args);

    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .CreateLogger();

    builder.Host.UseSerilog();

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")).UseSnakeCaseNamingConvention()
            .EnableSensitiveDataLogging()
                .LogTo(Console.WriteLine, LogLevel.Information));

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowReactApp", policy =>
        {
            policy.WithOrigins("https://localhost:5173", "http://localhost:5173")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
    });


    builder.Services.AddIdentity<User, IdentityRole<Guid>>(options =>
    {
        options.Password.RequiredLength = 8;
        options.Password.RequireDigit = true;
        options.Password.RequireUppercase = true;
        options.SignIn.RequireConfirmedEmail = false;
        options.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                        ValidAudience = builder.Configuration["JwtSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Secret"]))
                    };
                });

    builder.Services.AddScoped<IAuthService, AuthService>();
    builder.Services.AddScoped<ITokenService, TokenService>();
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.Configure<EmailSenderSettings>(builder.Configuration.GetSection(EmailSenderSettings.SectionName));
    builder.Services.AddScoped<IEmailSender, EmailSender>();
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

    builder.Services.AddAutoMapper(cfg =>
    {
        cfg.AddMaps(typeof(UserMappings).Assembly);
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseCors("AllowReactApp");
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "ERROR: Fatal error");
    throw;
}
finally
{
    Log.CloseAndFlush();
}
