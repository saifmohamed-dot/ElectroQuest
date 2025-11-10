using ElectroQuest.Application.Analytics.Services.GASPIAnalytics;
using ElectroQuest.Application.Analytics.Services.Interfaces;
using ElectroQuest.Infrastructure.Analytics.Settings;
using Microsoft.Extensions.Options;

namespace ElectroQuest.Background
{
    public class BackgroundPublisher : BackgroundService
    {
        readonly IGAPSIAnalyticsPerDayPublishService _publisherHandlerService;
        readonly IGAPSIAnalyticsPerDayQueryService _queryHandlerService;
        readonly GAPSIAnalyticsPaths _paths;
        public BackgroundPublisher
            (
                IGAPSIAnalyticsPerDayQueryService query ,
                IGAPSIAnalyticsPerDayPublishService publisher,
                IOptions<GAPSIAnalyticsPaths> options
            )
        {
            _publisherHandlerService = publisher;
            _queryHandlerService = query;
            _paths = options.Value;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            
            while(true)
            {
                Console.WriteLine("Publisher Waiting Signal");
                await Common.Start.WaitAsync(); // wait till signal
                var query = new GAPSIQuery(_paths.Google, _paths.PSI);
                var result = await _queryHandlerService.HandleAsync(query);
                await _publisherHandlerService.HandleAsync(new GAPSIPublishPerDayCommand(result));
                Console.WriteLine("publisher Finish .");
            }
        }
    }
}
