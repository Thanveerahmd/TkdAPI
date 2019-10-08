namespace TkdScoringApp.API.Entities
{
    public class Player : User
    {
        public string Color {get;set;}
        public int MatchId { get; set; }
        public int Totalscore { get; set; }
        public int Totalfoul { get; set; }
        
    }
}