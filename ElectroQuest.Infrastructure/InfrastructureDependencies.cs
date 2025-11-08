using ElectroQuest.Application.Analytics.Interfaces.Adapters;
using ElectroQuest.Infrastructure.Analytics.Adapters;
using ElectroQuest.Infrastructure.Analytics.Settings;
using ElectroQuest.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace ElectroQuest.Infrastructure
{
    // a static class provide extension methods related to the infrastructure layer .
    public static class InfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services , IConfiguration config)
        {
            services.Configure<RabbitMQSettings>(config.GetSection("RabbitMQ"));
            //services.AddScoped<IReadLocal, ReadLocal>();
            services.AddSingleton<IReadLocal, ReadLocal>();
            services.AddSingleton<IPublishMessage, RabbitPublisher>();
            services.AddSingleton<IConsumeMessage, RabbitConsumer>();
            services.AddDbContext<ElectroQuestDbContext>(options =>
            {
                // sql connection string
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });
            //services.AddScoped<DbContext, ElectroQuestDbContext>();
            return services;
        }
    }
}
