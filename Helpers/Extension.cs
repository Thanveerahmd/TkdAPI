using Microsoft.AspNetCore.Http;

namespace TkdScoringApp.API.Helpers
{
    public static class Extension
    {
        public static void AddAppError(this HttpResponse response, string msg)
        {
            response.Headers.Add("Application-Error", msg);
            response.Headers.Add("Access-Control-Expose-Error", "Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");

        }
    }
}