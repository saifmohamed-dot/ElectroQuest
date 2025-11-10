

using AutoMapper;
using ElectroQuest.Application.Analytics.DTO;
using ElectroQuest.Application.Analytics.Interfaces.Adapters;
using ElectroQuest.Application.Analytics.Services.Interfaces;
using ElectroQuest.Domain.Entities;
using Microsoft.Extensions.Logging;
using System.Collections.Specialized;
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
        readonly IGAPSIAnalyticsPerDayStoreService _storeHandler;
        readonly IMapper _mapper;
        readonly ILogger<GAPSIAnalyticsPerDayConsumerHandler> _logger;
        public GAPSIAnalyticsPerDayConsumerHandler(
            IConsumeMessage consumer , 
            IGAPSIAnalyticsPerDayStoreService handler ,
            IMapper mapper,
            ILogger<GAPSIAnalyticsPerDayConsumerHandler> logger
            )
        {
            _consumeMessage = consumer;
            _storeHandler = handler;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task HandleAsync()
        {
            DateOnly DateRecived = default;
            await _consumeMessage.ConsumeAsync(async (msg) =>
            {
                if(string.IsNullOrEmpty(msg))
                {
                    return false;
                }
                try
                {
                    IList<GAPSICombinedDto> dtos = JsonSerializer.Deserialize<IList<GAPSICombinedDto>>(msg)!;
                    var rowData = _mapper.Map<IList<RowData>>(dtos);
                    rowData = rowData.Select(rd => { rd.RecievedAt = DateTime.Now; return rd; }).ToList();
                    DailyStats statsAggregated = AggregateStatsToDailyStats(dtos);
                    DateRecived = statsAggregated.Date;
                    _logger.LogInformation($"Messages For Date {DateRecived} Consumed From Queue .");
                    await _storeHandler.HandelAsync(new GAPSIAnalyticsPerDayStoreCommand(rowData , statsAggregated));
                }
                catch (Exception ex) 
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
                _logger.LogInformation($"Messages For Date {DateRecived} Pushed To DB");
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
            aggregate.Date = stats.First().Date;
            aggregate.LastUpdatedAt = DateTime.Now;
            return aggregate;
        }

    }
}
