using System.Text;
using System;

namespace TkdScoringApp.API.Entities
{
    public class Score
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int MatchId { get; set; }
        public int ScoreValue { get; set; }

        public string Type { get; set; }
        public DateTime time { get; set; } 
        
        public Score()
        {
            time = DateTime.UtcNow;
        }
    }
}