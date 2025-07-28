using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.dto.request
{
    public class PasswordResetRequest
    {
        public string username {  get; set; }
        public string password { get; set; }
        public string ci {  get; set; }

        public PasswordResetRequest(string username,string password,string ci)
        { 
            this.username = username;
            this.password = password;
            this.ci = ci;
        }
    }
}
