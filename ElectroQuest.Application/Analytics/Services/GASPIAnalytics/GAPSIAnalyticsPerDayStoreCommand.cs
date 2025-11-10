using ElectroQuest.Application.Analytics.Services.Interfaces;
using ElectroQuest.Domain.Entities;

namespace ElectroQuest.Application.Analytics.Services.GASPIAnalytics
{
    public record GAPSIAnalyticsPerDayStoreCommand(IEnumerable<RowData> rowData , DailyStats aggregateDailyStats);
    public class GAPSIAnalyticsPerDayStoreHandler : IGAPSIAnalyticsPerDayStoreService
    {
        // this is the best i could to avoid 
        // the problem of singleton object (IDailyStats Repository) depends on 
        // scoped object like (DbContext) 
        
        // TODO : Decople This Dependency of Infrastructure layer .

        readonly IDbFactory _dbFactory;

        // in short this factory dealing uses the CreateContext method 
        // that the IDesignTimeFactory provide for the .net CLI .
        public GAPSIAnalyticsPerDayStoreHandler(IDbFactory factory)
        {
            _dbFactory = factory;
        }
        public async Task HandelAsync(GAPSIAnalyticsPerDayStoreCommand command)
        {
            // make the dbcontext track them first without saving
            // after tracking both of them commit the changes 
            // making those to operation one-unit-of-work 
            var context = _dbFactory.CreateDbContext();
            await context.Set<DailyStats>().AddAsync(command.aggregateDailyStats);
            await context.Set<RowData>().AddRangeAsync(command.rowData);
            await context.SaveChangesAsync();
        }
        
    }
}
