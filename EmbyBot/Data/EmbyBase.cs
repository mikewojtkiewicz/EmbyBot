using EmbyBot.Data.Emby;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;

namespace EmbyBot.Data
{
    public class EmbyBase
    {
        public static string EmbyGetWithToken(string embyUrl, string endpoint, string token, string filters = null)
        {
            string json = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using (HttpResponseMessage response = client.GetAsync($"{embyUrl}/emby/{endpoint}?api_key={token}{filters}").Result)
                {
                    using (HttpContent content = response.Content)
                    {
                        json = content.ReadAsStringAsync().Result;
                    }
                }
            }

            return json;
        }

        public static string EmbyGetWithLogin(string embyUrl, string endpoint, string token)
        {
            string json = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using (HttpResponseMessage response = client.GetAsync($"{embyUrl}/emby/{endpoint}?api_key={token}").Result)
                {
                    using (HttpContent content = response.Content)
                    {
                        json = content.ReadAsStringAsync().Result;
                    }
                }
            }

            return json;
        }

        public static string EmbyLogin(string embyUrl, string token, string userId, EmbyLogin login)
        {
            string json = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                Version build = Assembly.GetExecutingAssembly().GetName().Version;
                client.DefaultRequestHeaders.Add("X-Emby-Authorization", $"Emby UserId='{userId}', Client='Discord Bot', Device='Emby Bot', DeviceId='EmbyBot', Version='{build.Major}.{build.Minor}.{build.Build}.{build.Revision}'");

                string loginData = JsonConvert.SerializeObject(login);
                var stringContent = new StringContent(loginData, UnicodeEncoding.UTF8, "application/json");

                using (HttpResponseMessage response = client.PostAsync($"{embyUrl}/emby/Users/AuthenticateByName?api_key={token}", stringContent).Result)
                {
                    using (HttpContent content = response.Content)
                    {
                        json = content.ReadAsStringAsync().Result;
                    }
                }
            }

            return json;
        }
    }
}
