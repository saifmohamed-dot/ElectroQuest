using ElectroQuest.Application.Analytics.DTO;
using ElectroQuest.Application.Analytics.Services.GASPIAnalytics;

namespace ElectroQuest.Application.Analytics.Services.Interfaces
{
    public interface IGAPSIAnalyticsPerDayQueryService
    {
        Task<Dictionary<DateOnly, Dictionary<string, GAPSICombinedDto>>> HandleAsync(GAPSIQuery query);
    }
}
