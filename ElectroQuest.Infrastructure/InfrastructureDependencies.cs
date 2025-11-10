using ElectroQuest.Application.Analytics.Interfaces.Adapters;
using ElectroQuest.Application.Analytics.Services.Interfaces;
using ElectroQuest.Application.Users.Authentication;
using ElectroQuest.Domain.Repositories;
using ElectroQuest.Infrastructure.Analytics.Adapters;
using ElectroQuest.Infrastructure.Analytics.Repositories;
using ElectroQuest.Infrastructure.Analytics.Settings;
using ElectroQuest.Infrastructure.DBContext;
using ElectroQuest.Infrastructure.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
namespace ElectroQuest.Infrastructure
{
    // a static class provide extension methods related to the infrastructure layer .
    public static class InfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services , IConfiguration config)
        {
            services.Configure<RabbitMQSettings>(config.GetSection("RabbitMQ"));
            services.Configure<GAPSIAnalyticsPaths>(config.GetSection("Analytics"));
            services.Configure<DbSettings>(config.GetSection("DBSettings"));
            var JWTOptions = config.GetSection("JWT").Get<JWTSettings>();
            services.AddSingleton(JWTOptions);
            services.AddAuthentication()
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, option =>
                {
                    option.SaveToken = true;
                    option.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = JWTOptions.Issuer,
                        ValidateAudience = true,
                        ValidAudience = JWTOptions.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTOptions.Key))
                    };
                });
            services.AddSingleton<IReadLocal, ReadLocal>();
            services.AddSingleton<IPublishMessage, RabbitPublisher>();
            services.AddSingleton<IConsumeMessage, RabbitConsumer>();
            services.AddSingleton<IDbFactory, DbContextFactoryForSingleton>();
            services.AddDbContext<ElectroQuestDbContext>(options =>
            {
                // sql connection string
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<DbContext, ElectroQuestDbContext>();
            services.AddScoped<IDailyStatsRepository, DailyStatsRepository>();
            services.AddScoped<IRowDataRepository , RowDataRepository>();
            services.AddScoped<IAuthentication, JWTAuthentication>();
            services.AddScoped<IUsersRepository, UserRepository>();
            return services;
        }
    }
}
