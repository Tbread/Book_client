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
        private static SimpleHttpRequest _instance { get; set; }
        public static SimpleHttpRequest Instance
        {
            get { return _instance ?? (_instance = new SimpleHttpRequest()); }
        }

        private static string url = "http://127.0.0.1:8080";
        private CookieContainer cookieContainer = new CookieContainer();

        public async Task<Result> PostRequest(string uri,Dictionary<string,string> headers,Object contents)
        {
            using (HttpClientHandler handler = new HttpClientHandler()
            {
                CookieContainer = cookieContainer,
                UseCookies = true
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
                onDestroyRequest();
                return parsedResult;
            }
        }

        public async Task<Result> GetRequest(string uri, Dictionary<string, string> headers)
        {
            using (HttpClientHandler handler = new HttpClientHandler()
            {
                CookieContainer = cookieContainer,
                UseCookies = true
            })
            using (HttpClient client = new HttpClient(handler))
            {
                if (headers != null)
                {
                    foreach (KeyValuePair<string, string> items in headers)
                    {
                        client.DefaultRequestHeaders.Add(items.Key, items.Value);
                    }
                }
                var res = await client.GetAsync(url+uri);
                Result parsedResult = JsonConvert.DeserializeObject<Result>(await res.Content.ReadAsStringAsync());
                onDestroyRequest();
                return parsedResult;
            }
        }

        private void onDestroyRequest()
        {
            TokenPackage tokenPackage = new TokenPackage();
            foreach (Cookie cookie in cookieContainer.GetCookies(new Uri(url)))
            {
                if (cookie.Name.Equals("Refresh-Token"))
                {
                    tokenPackage.refreshToken = cookie;
                }
                if (cookie.Name.Equals("Access-Token"))
                {
                    tokenPackage.refreshToken = cookie;
                }
            }
            SingletonStorage.Instance.pushToken(tokenPackage);
        }
    }
}
//todo: 401의 경우 클라이언트 로그아웃시키기
