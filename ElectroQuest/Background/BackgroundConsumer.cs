
using ElectroQuest.Application.Analytics.Services.GASPIAnalytics;
using ElectroQuest.Application.Analytics.Services.Interfaces;
using ElectroQuest.Infrastructure.DBContext;

namespace ElectroQuest.Background
{
    public class BackgroundConsumer : BackgroundService
    {
        readonly IGAPSIAnalyticsPerDayConsumeService _analyticsPerDayConsumeServiceHandler;

        public BackgroundConsumer(IGAPSIAnalyticsPerDayConsumeService handler, IServiceScopeFactory scopeFactory)
        {
            _analyticsPerDayConsumeServiceHandler = handler;

        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            
            while(true)
            {

                Console.WriteLine("Consumer Waiting Signal");
                await Common.Start.WaitAsync();
                Console.WriteLine("Consumer Start");
                await _analyticsPerDayConsumeServiceHandler.HandleAsync();
                Console.WriteLine("Consumer End");
            }
            
        }
    }
}
