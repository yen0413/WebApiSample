using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace WebApiSample.Model
{
    public class UserToken
    {
        public HttpStatusCode StatusCode { get; set; }
        public string token { get; set; }
        public string ExpireTime { get; set; }
    }
}
