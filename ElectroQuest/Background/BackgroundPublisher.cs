using ElectroQuest.Application.Analytics.Services.GASPIAnalytics;
using ElectroQuest.Application.Analytics.Services.Interfaces;

namespace ElectroQuest.Background
{
    public class BackgroundPublisher : BackgroundService
    {
        readonly IGAPSIAnalyticsPerDayPublishService _publisherHandlerService;
        readonly IGAPSIAnalyticsPerDayQueryService _queryHandlerService;
        public BackgroundPublisher(IGAPSIAnalyticsPerDayQueryService query , IGAPSIAnalyticsPerDayPublishService publisher)
        {
            _publisherHandlerService = publisher;
            _queryHandlerService = query;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // now we pass it manually .
            // after test we will get it from the appsettings.json
            var query = new GAPSIQuery("D:\\playground/ga_data.json", "D:\\playground/psi_data.json");
            var result = await _queryHandlerService.HandleAsync(query);
            await _publisherHandlerService.HandleAsync(new GAPSIPublishPerDayCommand(result));
            Console.WriteLine("Background End .");
        }
    }
}
