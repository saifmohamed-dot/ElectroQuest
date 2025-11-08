using ElectroQuest.Application.Analytics.DTO;
using ElectroQuest.Application.Analytics.Interfaces.Adapters;
using ElectroQuest.Application.Analytics.Services.Interfaces;
using System.Threading.Tasks;

namespace ElectroQuest.Application.Analytics.Services.GASPIAnalytics
{
    // Excepect a Dictionary of date => pagesStats 
    // publishing by date .
    public record GAPSIPublishPerDayCommand(Dictionary<DateOnly, Dictionary<string, GAPSICombinedDto>> days);
    public class GAPSIAnalyticsPerDayPublishHandler : IGAPSIAnalyticsPerDayPublishService
    {
        readonly IPublishMessage _publisher;
        public GAPSIAnalyticsPerDayPublishHandler(IPublishMessage publisher)
        {
            _publisher = publisher;
        }
        public async Task HandleAsync(GAPSIPublishPerDayCommand command)
        {
            if (command.days == null)
            {
                throw new ArgumentNullException(nameof(command.days));
            }
            // publish the stats for every page in a specific day (date).
            IList<Task> tasks = new List<Task>();
            foreach(var day in command.days)
            {
                /* TODO : validate the GAPSICombinedDto with your custom Validator before publishing */

                IList<GAPSICombinedDto> pagesPerDay = day.Value /* The Inner Dictionary holding the page -> GAPSICombinedDot */
                    .Select(kv => kv.Value /* Selecting GAPSICombinedDto */
                    ).ToList();
                Console.WriteLine($"{pagesPerDay.Count} , {pagesPerDay[0].Views}");
                // publish to the queue 
                tasks.Add(_publisher.PublishAsync(pagesPerDay).ContinueWith(_ => Console.WriteLine($"Date : {day.Key} Published")));
            }
            await Task.WhenAll(tasks);
            //await _publisher.CompletePublishNotify();
        }
    }

}
