namespace TkdScoringApp.API.Dto
{
    public class AdminDto
    {
        public int id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
    }

     public class AdminTokenReturnDto
    {
        public int id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }

        public string Token { get; set; }
    }
}

