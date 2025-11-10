using AutoMapper;
using ElectroQuest.Application.Analytics.DTO;
using ElectroQuest.Application.Analytics.Services.GASPIAnalytics;
using ElectroQuest.Application.Analytics.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ElectroQuest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        readonly IGAPSIOverviewService _reportOverviewHandler;
        readonly IGAPSIAnalyticsPerPageService _reportOverviewPerPageHandler;
        readonly ResetAnalyticsHandler _resetHandler;
        readonly IMapper _mapper;
        public ReportsController(
            IGAPSIOverviewService reportOverviewhandler,
            IGAPSIAnalyticsPerPageService reportAnalyticsPerPageHandler,
            ResetAnalyticsHandler hander,
            IMapper mapper
            )
        {
            _reportOverviewHandler = reportOverviewhandler;
            _reportOverviewPerPageHandler = reportAnalyticsPerPageHandler;
            _resetHandler = hander;
            _mapper = mapper;
        }
        [Authorize]
        [HttpGet("Overview")]
        public async Task<IActionResult> GetOverview()
        {
            var list = _mapper.Map<IEnumerable<TotalAcrossAllPagesAndDatesDto>>(await _reportOverviewHandler.HandleAsync());
            return Ok(list);
        }
        [Authorize]
        [HttpGet("Pages")]
        public async Task<IActionResult> GetOverviewPerPage()
        {
            var list = _mapper.Map<IEnumerable<TotalPerPageDto>>(await _reportOverviewPerPageHandler.HandleAsync());
            return Ok(list);
        }
        [Authorize]
        [HttpGet("ResetAnalytics")]
        public async Task<IActionResult>ResetAnalytics()
        {
            var result = await _resetHandler.HandleAsync();
            if(!result.Success)
            {
                return Conflict(result);
            }
            return Ok(result);
        }
        [Authorize]
        [HttpGet("StartAnalytics")]
        public IActionResult StartAnalytics()
        {
            Common.Start.Release(2); // signal all waiting background workers to start .
            return Ok("Analytics Started ....");
        }
    }
}
