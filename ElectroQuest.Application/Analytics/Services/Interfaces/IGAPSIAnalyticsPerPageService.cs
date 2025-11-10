using ElectroQuest.Domain.Entities;

namespace ElectroQuest.Application.Analytics.Services.Interfaces
{
    public interface IGAPSIAnalyticsPerPageService
    {
        Task<IEnumerable<RowData>> HandleAsync();
    }
}
