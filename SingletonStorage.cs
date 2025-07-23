using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    }
}
// CookieContainer가 자동적으로 request이후 갱신되는것같으니 굳이 해당 객체가 필요없을듯???
