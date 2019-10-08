namespace TkdScoringApp.API.Dto
{
    public class PlayerDto
    {
        public int id { get; set; }
        public string Name { get; set; }

        public string Color {get;set;}
        public int MatchId { get; set; }
        public int score { get; set; }
    }
}

