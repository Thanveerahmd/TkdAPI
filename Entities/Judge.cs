using System;

namespace TkdScoringApp.API.Entities
{
    public class Judge:User
    {
         public DateTime LoginTime { get; set; }
         public int MatchId { get; set; }
    }
}