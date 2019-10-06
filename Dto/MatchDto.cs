using Microsoft.EntityFrameworkCore;

namespace TkdScoringApp.API.Dto
{
    public class MatchDto
    {
        public int Id { get; set; }
        public int AdminId { get; set; }
        public string RingId {get;set;}
        public int NoOfJudges { get; set; }

    }
}


