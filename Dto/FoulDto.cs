using System;

namespace TkdAPI.Dto
{
    public class FoulDto
    {
        public int FoulValue { get; set; }
        public DateTime time { get; set; }
        public int MatchId { get; set; }
        public int PlayerId { get; set; }
         public FoulDto()
        {
            time = DateTime.UtcNow;
        }

    }
}