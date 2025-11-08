
using ElectroQuest.Application.Analytics.Services.Interfaces;

namespace ElectroQuest.Background
{
    public class BackgroundConsumer : BackgroundService
    {
        readonly IGAPSIAnalyticsPerDayConsumeService _analyticsPerDayConsumeServiceHandler;
        public BackgroundConsumer(IGAPSIAnalyticsPerDayConsumeService handler)
        {
            _analyticsPerDayConsumeServiceHandler = handler;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Consumer Start");
            await _analyticsPerDayConsumeServiceHandler.HandleAsync();
            Console.WriteLine("Consumer End");
        }
    }
}
