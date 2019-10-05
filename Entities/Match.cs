using System.Collections.Generic;

namespace TkdScoringApp.API.Entities
{
    public class Match
    {
        public int Id { get; set; }
        public int AdminId { get; set; }
        public string RingId {get;set;}
        public bool isFinished {get;set;} 
        public bool isPause { get; set; }
        public int NoOfJudges { get; set; }
        public ICollection<Judge> Judges { get; set; }
        public ICollection<Player> Players { get; set; }
    }
}