

using ElectroQuest.Application.Analytics.DTO;
using ElectroQuest.Application.Analytics.Interfaces.Adapters;
using ElectroQuest.Application.Analytics.Services.Interfaces;
using ElectroQuest.Domain.Entities;
using System.Text.Json;

namespace ElectroQuest.Application.Analytics.Services.GASPIAnalytics
{
    record GAPSIAnalyticsPerDayConsumeQuery
    {
    }
    public class GAPSIAnalyticsPerDayConsumerHandler : IGAPSIAnalyticsPerDayConsumeService
    {
        // constructor ... //
        readonly IConsumeMessage _consumeMessage;
        public GAPSIAnalyticsPerDayConsumerHandler(IConsumeMessage consumer)
        {
            _consumeMessage = consumer;
        }
        public async Task HandleAsync()
        {
            await _consumeMessage.ConsumeAsync((msg) =>
            {
                if(string.IsNullOrEmpty(msg))
                {
                    return false;
                }
                try
                {
                    IList<GAPSICombinedDto> dtos = JsonSerializer.Deserialize<IList<GAPSICombinedDto>>(msg)!;
                    DailyStats statsAggregated = AggregateStatsToDailyStats(dtos);
                }
                catch (Exception ex) 
                {
                    throw;
                }
                return true;
            });
        }
        DailyStats AggregateStatsToDailyStats(IEnumerable<GAPSICombinedDto> stats)
        {
            if (stats == null)
            {
                throw new Exception("Cannot Aggregate On Null Stats");
            }
            if (stats.Count() == 0)
            {
                throw new Exception("Given Stats is Empty");
            }
            DailyStats aggregate = new()
            {
                TotalSessions = stats.Sum(st => st.Sessions),
                TotalViews = stats.Sum(st => st.Views),
                TotalUsers = stats.Sum(st => st.Users),
                AvgPerformance = stats.Average(st => st.PerformanceScore)
            };
            return aggregate;
        }
    }
}
