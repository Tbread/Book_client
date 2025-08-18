using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Book.dto.response;

namespace Book
{
    public class SingletonStorage
    {
        private static SingletonStorage _instance { get; set; }
        public static SingletonStorage Instance
        {
            get
            {
                return _instance ?? (_instance = new SingletonStorage());
            }
        }

        private CookieContainer tokenContainer = new CookieContainer();

        public CookieContainer getCookieContainer()
        {
            return this.tokenContainer;
        }

        public void pushToken(TokenPackage tokenPackage)
        {
            if (tokenPackage != null)
            {
                if (tokenPackage.refreshToken != null)
                {
                    this.tokenContainer.Add(tokenPackage.refreshToken);
                }
                if (tokenPackage.accessToken != null)
                {
                    this.tokenContainer.Add(tokenPackage.accessToken);
                }
            }
        }

        public string getAccessToken()
        {
            foreach (Cookie cookie in this.tokenContainer.GetCookies(new Uri("http://127.0.0.1:8080")))
            {
                if (cookie.Name.Equals("Access-Token"))
                {
                    return cookie.Value;
                }
            }
            return null;
        }

        public string getRefreshToken()
        {
            foreach (Cookie cookie in this.tokenContainer.GetCookies(new Uri("http://127.0.0.1:8080")))
            {
                if (cookie.Name.Equals("Refresh-Token"))
                {
                    return cookie.Value;
                }
            }
            return null;
        }
    }
}
// CookieContainer가 자동적으로 request이후 갱신되는것같으니 굳이 해당 객체가 필요없을듯???
