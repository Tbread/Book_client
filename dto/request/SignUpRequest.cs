using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Book.dto.request
{
    public class SignUpRequest
    {
        public string username;
        public string password;
        public string ci;

        public SignUpRequest(string username, string password)
        {
            this.username = username;
            this.password = password;
            this.ci = Guid.NewGuid().ToString();
            // 본인인증 api 미사용으로 임의의 ci 사용
        }
    }
}
