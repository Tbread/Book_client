using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Book.enums;
using Newtonsoft.Json.Linq;

namespace Book.dto
{
    public class Result
    {
        public string message {  get; set; } 
        public JavaHttpStatus status { get; set; }
        public JToken data { get; set; }
        public bool success { get; set; }
    }
}
