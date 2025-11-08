using ElectroQuest.Domain.Entities;

namespace ElectroQuest.Domain.Repositories
{
    public interface IDailyStatsRepository
    {
        Task<IEnumerable<DailyStats>> GetDailyStatsAsync();
        Task AddDailStatsAsync(DailyStats dailStats , bool saveAfterInsert = true);
        Task SaveChangesAsync();
    }
}
