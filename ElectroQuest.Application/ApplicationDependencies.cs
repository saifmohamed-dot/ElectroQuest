using ElectroQuest.Application.Analytics.Services.GASPIAnalytics;
using ElectroQuest.Application.Analytics.Services.Interfaces;
using ElectroQuest.Application.Analytics.Services.Usecases;
using ElectroQuest.Application.Users.Services;
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
            services.AddSingleton<IGAPSIAnalyticsPerDayStoreService, GAPSIAnalyticsPerDayStoreHandler>();
            services.AddScoped<IGAPSIOverviewService, ReportOverviewHandler>();
            services.AddScoped<IGAPSIAnalyticsPerPageService, ReportOverviewPerPageHandler>();
            services.AddScoped<UserLoginHandler>();
            services.AddScoped<UserRegisterHandler>();
            services.AddScoped<ResetAnalyticsHandler>();
            services.AddAutoMapper(typeof(MappingConfig));
            return services;
        }
    }
}
