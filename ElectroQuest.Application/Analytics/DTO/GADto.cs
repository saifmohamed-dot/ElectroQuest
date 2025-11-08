namespace ElectroQuest.Application.Analytics.DTO
{
    // this is an for the incoming analytics after binding it to our .net classes 
    // from (json , xml , ....) => .net class (GA json structure) => (GA .net class structure)
    internal class GADto
    {
        public DateOnly Date { get; set; }
        public string Page { get; set; }
        public int Users { get; set; }
        public int Sessions { get; set; }
        public int Views { get; set; }
    }
}
