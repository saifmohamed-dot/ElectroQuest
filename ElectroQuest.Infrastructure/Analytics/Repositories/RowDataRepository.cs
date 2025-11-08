using ElectroQuest.Domain.Entities;
using ElectroQuest.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ElectroQuest.Infrastructure.Analytics.Repositories
{
    public class RowDataRepository : IRowDataRepository
    {
        readonly DbContext _dbContext;
        public RowDataRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddStatsRowDataAsync(RowData rowData , bool saveAfterInsert = true)
        {
            await _dbContext.Set<RowData>().AddAsync(rowData);
            if(saveAfterInsert)
            {
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<RowData>> GetStatsRowDataByPageNameAsync(string page)
        {
            return await _dbContext.Set<RowData>()
                            .Where(rd => rd.Page == page)
                            .ToListAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
