using ElectroQuest.Application.Analytics.DTO;
using ElectroQuest.Application.Analytics.Services.Interfaces;
using ElectroQuest.Domain.Entities;
using ElectroQuest.Domain.Repositories;

namespace ElectroQuest.Application.Analytics.Services.Usecases
{
    public class ReportOverviewPerPageHandler : IGAPSIAnalyticsPerPageService
    {
        readonly IRowDataRepository _rowDataRepository;
        public ReportOverviewPerPageHandler(IRowDataRepository repo)
        {
            _rowDataRepository = repo;
        }
        public async Task<IEnumerable<RowData>> HandleAsync()
        {
            return await _rowDataRepository.GetStatsPerPageAsync();
        }
    }
}
