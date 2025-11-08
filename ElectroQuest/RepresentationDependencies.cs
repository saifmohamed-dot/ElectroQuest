using ElectroQuest.Application;
using ElectroQuest.Background;
using ElectroQuest.Domain;
using ElectroQuest.Infrastructure;

namespace ElectroQuest
{
    public static class RepresentationDependencies
    {
        public static IServiceCollection AddRepresentationServices(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddApplicationServices()
                .AddDomainServices()
                .AddInfrastructureServices(configuration);
            services.AddHostedService<BackgroundPublisher>();
            services.AddHostedService<BackgroundConsumer>();
            return services;
        }
    }
}
