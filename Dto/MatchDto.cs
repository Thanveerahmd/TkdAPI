using Microsoft.EntityFrameworkCore;

namespace TkdScoringApp.API.Dto
{
    public class MatchDto
    {
        public int Id { get; set; }
        public int AdminId { get; set; }
        public int ScoreOfPlayer1 { get; set; }
        public int ScoreOfPlayer2 { get; set; }
        public bool isPause { get; set; }
    }
}


