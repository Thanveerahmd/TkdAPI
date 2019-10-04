using System;

namespace TkdScoringApp.API.Entities
{
    public class TempScore
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public DateTime time { get; set; }
        public int MatchId { get; set; }
        public int PlayerId { get; set; }
        public int JudgeId { get; set; }
        public int NoOfConfirmation { get; set; }
        public int NoOfClickIn3Sec { get; set; }
    }
}