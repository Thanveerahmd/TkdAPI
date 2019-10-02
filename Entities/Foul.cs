namespace TkdScoringApp.API.Entities
{
    public class Foul
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int MatchId { get; set; }
        public int FoulValue { get; set; }
    }
}