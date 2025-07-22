using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Book.dto;
using Book.dto.response;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Book
{
    public class SimpleHttpRequest
    {
        static string url = "http://127.0.0.1:8080";

        public static async Task<Result> PostRequest(string uri,Dictionary<string,string> headers,Object contents,Dictionary<string,string> cookies)
        {
                CookieContainer cookieContainer = new CookieContainer();
            if (cookies != null)
            {
                foreach (KeyValuePair<string, string> items in cookies)
                {
                    cookieContainer.Add(new Uri(url), new Cookie(items.Key, items.Value));
                }
            }
            using (HttpClientHandler handler = new HttpClientHandler()
            {
                CookieContainer = cookieContainer
            })
            using (HttpClient client = new HttpClient(handler))
            {
                client.DefaultRequestHeaders.Add("Contents-Type", "application/json");
                if (headers != null)
                { 
                    foreach (KeyValuePair<string, string> items in headers)
                    {
                        client.DefaultRequestHeaders.Add(items.Key, items.Value);
                    }
                }
                var res = await client.PostAsync(url + uri, new StringContent(JsonConvert.SerializeObject(contents)));
                Result parsedResult = JsonConvert.DeserializeObject<Result>(await res.Content.ReadAsStringAsync());

                return parsedResult;
            }
        }
    }
}
