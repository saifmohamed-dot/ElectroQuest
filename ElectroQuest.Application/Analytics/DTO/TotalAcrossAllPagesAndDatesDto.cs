using System.ComponentModel.DataAnnotations;

namespace ElectroQuest.Application.Analytics.DTO
{
    public class TotalAcrossAllPagesAndDatesDto
    {
        public DateOnly Date {  get; set; }
        public int TotalUsers { get; set; }
        public int TotalSessions { get; set; }
        public int TotalViews { get; set; }
        public float AvgPerformance { get; set; }
    }
}
