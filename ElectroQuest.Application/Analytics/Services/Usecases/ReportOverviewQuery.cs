using ElectroQuest.Application.Analytics.Services.Interfaces;
using ElectroQuest.Domain.Entities;
using ElectroQuest.Domain.Repositories;

namespace ElectroQuest.Application.Analytics.Services.Usecases
{
    public class ReportOverviewHandler(IDailyStatsRepository _dailyStatsRepository) : IGAPSIOverviewService
    {
        public async Task<IEnumerable<DailyStats>> HandleAsync()
        {
            return await _dailyStatsRepository.GetDailyStatsAsync();
        }
    }
}
