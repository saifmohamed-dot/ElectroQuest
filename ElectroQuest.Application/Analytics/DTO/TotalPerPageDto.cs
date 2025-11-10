using System.ComponentModel.DataAnnotations;

namespace ElectroQuest.Application.Analytics.DTO
{
    public class TotalPerPageDto
    {
        public string Page {  get; set; }
        [Display(Name = "TotalUsers")]
        public int Users { get; set; }
        [Display(Name = "TotalSessions")]
        public int Sessions { get; set; }
        [Display(Name = "TotalViews")]
        public int Views { get; set; }
        [Display(Name = "AvgPerformanceScore")]
        public float PerformanceScore { get; set; }
        [Display(Name = "AvgLCP_ms")]
        public int LCP_ms { get; set; }

    }
}
