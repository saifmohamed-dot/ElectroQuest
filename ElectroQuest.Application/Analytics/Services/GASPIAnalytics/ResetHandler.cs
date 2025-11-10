
using ElectroQuest.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ElectroQuest.Application.Analytics.Services.GASPIAnalytics
{
    public class ResetAnalyticsResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public int DailyRowEffected {  get; set; }
        public int RowDataEffected {  get; set; }
    }
    public class ResetAnalyticsHandler
    {
        readonly DbContext _context;
        public ResetAnalyticsHandler(DbContext db)
        {
            _context = db;
        }
        public async Task<ResetAnalyticsResult> HandleAsync()
        {
            try
            {
                int dayStatsDeleted = await _context.Set<DailyStats>().Where(d => d.Id >= 0).ExecuteDeleteAsync();
                int rowDataDeleted = await _context.Set<RowData>().Where(r => r.Id >= 0).ExecuteDeleteAsync();
                return new ResetAnalyticsResult()
                {
                    Success = true,
                    DailyRowEffected = dayStatsDeleted,
                    RowDataEffected = rowDataDeleted
                };
            }
            catch (Exception ex)
            {
                return new ResetAnalyticsResult()
                {
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}
