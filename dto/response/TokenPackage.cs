using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.dto.response
{
    public class TokenPackage
    {
        public string refreshToken {  get; set; }
        public string accessToken {  get; set; }
    }
}
