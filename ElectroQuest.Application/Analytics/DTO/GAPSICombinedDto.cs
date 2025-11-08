using System.ComponentModel.DataAnnotations;
namespace ElectroQuest.Application.Analytics.DTO
{
    // we got to do this dto because :
    // there are some fields will not be populuted till we push it to the DB , like :
    // 1 - CreatedAt (after consuming)
    // 2 - Id (Identity Generated)

    public class GAPSICombinedDto
    {
        [Required(ErrorMessage = "Page Name Cannot Be Empty !")]
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
        public int LCP_ms { get; set; }
        public override string ToString()
        {
            return $"Date : {Date} , Page: {Page} , Users : {Users} , Session : {Sessions} , Views : {Views} , Performance : {PerformanceScore} , LCP : {LCP_ms}";
        }
    }
}
