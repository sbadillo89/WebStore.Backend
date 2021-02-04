using Microsoft.AspNetCore.Mvc;
using SB.VirtualStore.Data.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SB.VirtualStore
{
    public static class Utils
    {

        public static GlobalResponse GenerateResponse(object RequestData, object ResponseData, HttpStatusCode statusCode, string Message)
        {
            GlobalResponse globalResponse = new GlobalResponse();
            globalResponse.RequestData = RequestData;
            globalResponse.ResponseData = ResponseData;
            globalResponse.Status = statusCode;
            globalResponse.Message = Message;

            return globalResponse;
        }
    }
}
