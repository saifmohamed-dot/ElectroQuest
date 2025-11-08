namespace ElectroQuest.Application.Analytics.DTO
{
    // this is an for the incoming analytics after binding it to our .net classes 
    // from (json , xml , ....) => .net class (SPI json structure) => (SPI .net class structure) 
    public class PSIDto
    {
        public DateOnly Date { get; set; }
        public string Page { get; set; }
        public float PerformanceScore { get; set; }
        public int LCP_ms { get; set; }
    }
}
