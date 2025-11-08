using ElectroQuest.Application.Analytics.Services.GASPIAnalytics;
namespace ElectroQuest.Application.Analytics.Services.Interfaces
{
    public interface IGAPSIAnalyticsPerDayPublishService
    {
        Task HandleAsync(GAPSIPublishPerDayCommand command);
    }
}
