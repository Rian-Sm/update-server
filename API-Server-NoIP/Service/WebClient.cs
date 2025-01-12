using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Server_NoIP.Service
{
    public class WebClient
    {
        private const string url = "https://api.ipify.org";
        
        public static async Task<string> GetIP(){

            using HttpClient client = new HttpClient();
            string publicIp = await client.GetStringAsync(url);

            return publicIp;
        }

        public static async Task<string> SendData(string uri){

            using HttpClient client = new HttpClient();
            string response = await client.GetStringAsync(url);

            return response;


        }
    }
}