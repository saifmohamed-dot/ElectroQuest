using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectroQuest.Domain.Entities
{
    public class RowData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Date Cannot Be Empty !")]
        public DateOnly Date { get; set; }
        [Required(ErrorMessage = "Page Name Cannot Be Empty !")]
        public string Page { get; set; }
        [Required(ErrorMessage = "Users Number Cannot Be Empty !")]
        public int Users { get; set; }
        [Required(ErrorMessage = "Sessions Count Cannot Be Empty !")]
        public int Sessions { get; set; }
        [Required(ErrorMessage = "Views Count Cannot Be Empty !")]
        public int Views { get; set; }
        [Required(ErrorMessage = "PerformanceScore Cannot Be Empty !")]
        public float PerformanceScore { get; set; }
        [Required(ErrorMessage = "Largest Contentful Paint Cannot Be Empty !")]
        public int LCP { get; set; }
        [Required(ErrorMessage = "RecievedAt Cannot Be Empty !")]
        public DateTime RecievedAt {  get; set; }

    }
}
