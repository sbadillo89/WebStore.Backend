using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SB.VirtualStore.Data.Models.Response
{
    public class GlobalResponse
    {

        public object RequestData { get; set; }

        public object ResponseData { get; set; }

        public HttpStatusCode Status { get; set; }

        public string Message { get; set; }
         
    }
}
