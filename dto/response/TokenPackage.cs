using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Book.dto.response
{
    public class TokenPackage
    {
        public Cookie refreshToken {  get; set; }
        public Cookie accessToken {  get; set; }
    }
}
