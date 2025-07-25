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
        public string username { get; set; }
        public string password { get; set; }
        public string ci {  get; set; }

        public SignUpRequest(string username, string password)
        {
            this.username = username;
            this.password = password;
            this.ci = Guid.NewGuid().ToString();
            // 본인인증 api 미사용으로 임의의 ci 사용
        }
    }
}
