using ElectroQuest.Application.Analytics.Services.GASPIAnalytics;
using ElectroQuest.Application.Analytics.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
namespace ElectroQuest.Application
{
    public static class ApplicationDependencies
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //services.AddScoped<IGAPSIAnalyticsPerDayQueryService, GAPSIQueryHandler>();
            //services.AddScoped<IGAPSIAnalyticsPerDayPublishService, GAPSIAnalyticsPerDayPublishHandler>();
            services.AddSingleton<IGAPSIAnalyticsPerDayQueryService, GAPSIQueryHandler>();
            services.AddSingleton<IGAPSIAnalyticsPerDayPublishService, GAPSIAnalyticsPerDayPublishHandler>();
            services.AddSingleton<IGAPSIAnalyticsPerDayConsumeService, GAPSIAnalyticsPerDayConsumerHandler>();
            return services;
        }
    }
}
