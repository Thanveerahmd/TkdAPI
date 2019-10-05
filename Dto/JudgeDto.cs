using System;

namespace TkdScoringApp.API.Dto
{
    public class JudgeDto
    {
        public int id { get; set; }
        public string Name { get; set; }
        public int matchId { get; set; }
        public string RingId {get;set;}
        public DateTime LoginTime { get; set; }

        public JudgeDto(){
            LoginTime =  DateTime.UtcNow;
        }
    }
}

