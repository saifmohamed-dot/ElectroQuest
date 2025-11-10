using ElectroQuest.Application.Analytics.DTO;
using ElectroQuest.Domain.Entities;
using ElectroQuest.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ElectroQuest.Infrastructure.Analytics.Repositories
{
    public class RowDataRepository : IRowDataRepository
    {
        readonly DbContext _dbContext;
        public RowDataRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddRangeStatsRowDataAsync(IEnumerable<RowData> rows, bool saveAfterInsert = true)
        {
            await _dbContext.Set<RowData>().AddRangeAsync(rows);
            if(saveAfterInsert)
            {
                await SaveChangesAsync();
            }
        }

        public async Task AddStatsRowDataAsync(RowData rowData , bool saveAfterInsert = true)
        {
            await _dbContext.Set<RowData>().AddAsync(rowData);
            if(saveAfterInsert)
            {
                await _dbContext.SaveChangesAsync();
            }
        }
        // i will map it to the proper dto in the application layer 
        // but now i need to return RowData , so i didn't depend on the Application layer (upper layer)
        public async Task<IEnumerable<RowData>> GetStatsPerPageAsync()
        {
            return await _dbContext.Set<RowData>().GroupBy(rd => rd.Page).Select(rd => new RowData()
            {
                Page = rd.Key,
                Users = rd.Sum(r => r.Users),
                Sessions = rd.Sum(r => r.Sessions),
                Views = rd.Sum(r => r.Views),
                PerformanceScore = rd.Average(r => r.PerformanceScore),
                LCP_ms = (int)rd.Average(r => r.LCP_ms)
                
            }).ToListAsync();
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
