using ElectroQuest.Application.Analytics.Services.GASPIAnalytics;

namespace ElectroQuest.Application.Analytics.Services.Interfaces
{
    public interface IGAPSIAnalyticsPerDayStoreService
    {
        Task HandelAsync(GAPSIAnalyticsPerDayStoreCommand command);
    }
}
