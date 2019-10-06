using System;
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

        public static string OTPCharacters(string MaxLength)
        {
            string OTPLength = MaxLength;

            string NewCharacters = "";

            //This one tells you which characters are allowed in this new password
            string allowedChars = "";

            //Here Specify your OTP Characters
            allowedChars = "1,2,3,4,5,6,7,8,9,0";

            //If you Need more secure OTP then uncomment Below Lines 
            //  allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";        
            // allowedChars += "~,!,@,#,$,%,^,&,*,+,?";


            char[] sep = { ',' };
            string[] arr = allowedChars.Split(sep);

            string IDString = "";
            string temp = "";

            //utilize the "random" class
            Random rand = new Random();


            for (int i = 0; i < Convert.ToInt32(OTPLength); i++)
            {
                temp = arr[rand.Next(0, arr.Length)];
                IDString += temp;
                NewCharacters = IDString;
            }

            return NewCharacters;
        }
    }
}