using System;

namespace TkdScoringApp.API.Dto
{
    public class JudgeDto
    {
        public int id { get; set; }
        public string Name { get; set; }
        public int matchId { get; set; }
        public DateTime LoginTime { get; set; }
    }
}

