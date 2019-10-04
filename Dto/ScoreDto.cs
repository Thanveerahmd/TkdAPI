using System;

namespace TkdScoringApp.API.Dto
{
    public class ScoreDto
    { 
        public int Score { get; set; }
        public DateTime time { get; set; }
        public int MatchId { get; set; }
        public int PlayerId { get; set; }
        public int JudgeId { get; set; }
        public int NoOfConfirmation { get; set; }
        public int NoOfConsecutiveTaps { get; set; } // defult is 1
        public ScoreDto()
        {
            time = DateTime.UtcNow;
        }
    }
}