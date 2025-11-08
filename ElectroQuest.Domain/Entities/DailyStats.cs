using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectroQuest.Domain.Entities
{
    public class DailyStats
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Date Cannot Be Empty !")]
        public DateOnly Date { get; set; }
        [Required(ErrorMessage = "TotalUsers Number Cannot Be Empty !")]
        public int TotalUsers { get; set; }
        [Required(ErrorMessage = "TotalSessions Count Cannot Be Empty !")]
        public int TotalSessions { get; set; }
        [Required(ErrorMessage = "TotalViews Count Cannot Be Empty !")]
        public int TotalViews { get; set; }
        [Required(ErrorMessage = "AvgPerformance Cannot Be Empty !")]
        public float AvgPerformance { get; set; }
        [Required(ErrorMessage = "LastUpdatedAt Cannot Be Empty !")]
        public DateTime LastUpdatedAt { get; set; }
    }
}
