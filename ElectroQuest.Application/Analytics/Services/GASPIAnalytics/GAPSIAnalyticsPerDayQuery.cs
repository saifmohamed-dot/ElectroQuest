using ElectroQuest.Application.Analytics.DTO;
using ElectroQuest.Application.Analytics.Interfaces.Adapters;
using ElectroQuest.Application.Analytics.Services.Interfaces;
namespace ElectroQuest.Application.Analytics.Services.GASPIAnalytics
{


    // what this handler will do in a nutshell => 
    // 1 - read all the json objects locally .
    // 2 - map each object to specified dto based on the useCase (GAPSI) 
    // it will be GA(Google Analytics) dto , PSI (PageSpeed Insights) dto.

    // given that each page got an analytics per day 
    // we have one - to - many relation identity 
    // 1/10/2020 -> /home , /contacts , /privacy , etc.. till we exhaust all pages .
    
    public record GAPSIQuery(string gALocation , string pSILocation); // this location will be in appsettings.json .
    public class GAPSIQueryHandler : IGAPSIAnalyticsPerDayQueryService
    {
        // DI Constructor Injection handling ....
        readonly IReadLocal _ReadLocal;
        readonly Dictionary<DateOnly, Dictionary<string, GAPSICombinedDto>> _PagesStatsGroupedByDate;
        public GAPSIQueryHandler(IReadLocal readLocal)
        {
            _ReadLocal = readLocal;
            _PagesStatsGroupedByDate = new();
        }
        // this dictionary will satisfy the above one - to - many realation 
        // so for every date (key) we will publish the page dictionary (value) as list 
        // like : 1/10/2020 => (/home(inner key) , DailyStats) , (/contacts , DailyStats) , ....
        public async Task<Dictionary<DateOnly , Dictionary<string , GAPSICombinedDto>>> HandleAsync(GAPSIQuery query)
        {    
            try
            {
                string gaExtension = Path.GetExtension(query.gALocation);
                string pSIExtension = Path.GetExtension(query.pSILocation);
                var gaTask = _ReadLocal.ReadLocalAsync<IList<GADto>>(query.gALocation, gaExtension);
                var psiTask = _ReadLocal.ReadLocalAsync<IList<PSIDto>>(query.pSILocation, pSIExtension);
                await Task.WhenAll(gaTask, psiTask);
                // tests for each of tasks after completion //
                ValidateTask(gaTask);
                ValidateTask(psiTask);
                // start initialization with google analytics 
                InitializeStatsWith(gaTask.Result!);
                // append the psi analytics 
                AppendToStats(psiTask.Result!);
            }
            catch (Exception ex)
            {
                throw;
            }
            return _PagesStatsGroupedByDate;
        }
        bool ValidateTask<TResult>(Task<TResult> task)
        {
            if (task.Exception != null)
            {
                throw task.Exception;
            }
            if (task.Result == null)
            {
                throw new Exception($"Error Happend During Deserailization of {typeof(TResult)}!");
            }
            return true;
        }
        // initailize the 
        void InitializeStatsWith(IEnumerable<GADto> gas)
        {
            if (gas == null)
            {
                throw new Exception($"We Cannot Initialize Stats With Null : {gas}");
            }
            foreach(var ga in gas)
            {
                if (!_PagesStatsGroupedByDate.ContainsKey(ga.Date))
                {
                    _PagesStatsGroupedByDate.Add(ga.Date, new()); // init 
                }
                // for a given date : no page can appear more than once .
                else if (_PagesStatsGroupedByDate[ga.Date].ContainsKey(ga.Page))
                {
                    throw new Exception($"Duplicated Date-Page Analytics :{ga.Date} : {ga.Page}");
                }
                _PagesStatsGroupedByDate[ga.Date][ga.Page] = new GAPSICombinedDto()
                {
                    // append the Google Analytics Releated Fields 
                    Date = ga.Date,
                    Page = ga.Page,
                    Users = ga.Users,
                    Sessions = ga.Sessions,
                    Views = ga.Views,
                };
            }
        }
        void AppendToStats(IEnumerable<PSIDto> psis)
        {
            if(psis == null)
            {
                throw new Exception($"We Cannot Append To Stats A Null : {psis}");
            } 
            foreach (var psi in psis)
            {
                // append PSI analytics only with matching Google Analytics with same Date-Page 
                // so we could get a combined dto of both to publish later .
                if (!_PagesStatsGroupedByDate.ContainsKey(psi.Date))
                {
                    continue;
                }
                if (_PagesStatsGroupedByDate[psi.Date].ContainsKey(psi.Page))
                {
                    var stats = _PagesStatsGroupedByDate[psi.Date][psi.Page];
                    stats.PerformanceScore = psi.PerformanceScore;
                    stats.LCP_ms = psi.LCP_ms;
                }
            }
        }
    }
}
