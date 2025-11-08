using ElectroQuest.Domain.Entities;
using ElectroQuest.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ElectroQuest.Infrastructure.Analytics.Repositories
{
    public class DailyStatsRepository : IDailyStatsRepository
    {
        readonly DbContext _dbContext;
        public DailyStatsRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddDailStatsAsync(DailyStats dailStats , bool saveAfterInsert = true)
        {
            await _dbContext.Set<DailyStats>().AddAsync(dailStats);
            if(saveAfterInsert)
            {
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<DailyStats>> GetDailyStatsAsync()
        {
            return await _dbContext.Set<DailyStats>().ToListAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
