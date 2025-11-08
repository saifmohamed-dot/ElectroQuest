using ElectroQuest.Domain.Entities;

namespace ElectroQuest.Domain.Repositories
{
    public interface IRowDataRepository
    {
        Task AddStatsRowDataAsync(RowData rowData , bool saveAfterInsert = true);
        Task<IEnumerable<RowData>> GetStatsRowDataByPageNameAsync(string page);
        Task SaveChangesAsync();
    }
}
