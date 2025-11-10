using ElectroQuest.Domain.Entities;
using System.Linq.Expressions;


namespace ElectroQuest.Domain.Repositories
{
    public interface IRowDataRepository
    {
        Task AddStatsRowDataAsync(RowData rowData , bool saveAfterInsert = true);
        Task AddRangeStatsRowDataAsync(IEnumerable<RowData> rows, bool saveAfterInsert = true);
        Task<IEnumerable<RowData>> GetStatsPerPageAsync();
        Task<IEnumerable<RowData>> GetStatsRowDataByPageNameAsync(string page);
        Task SaveChangesAsync();
    }
}
