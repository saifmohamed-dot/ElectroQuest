using ElectroQuest.Domain.Entities;

namespace ElectroQuest.Application.Analytics.Services.Interfaces
{
    public interface IGAPSIOverviewService
    {
        Task<IEnumerable<DailyStats>> HandleAsync();
    }
}
