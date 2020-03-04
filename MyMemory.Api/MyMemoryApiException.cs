using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MyMemory.Api
{
    public class MyMemoryApiException : Exception
    {
        public MyMemoryApiException(HttpStatusCode code, string responseData) :
            base("The API called failed")

        {
            StatusCode = code;
            ResponseData = responseData;
        }

        public string ResponseData { get; }

        public HttpStatusCode StatusCode { get; }
    }
}
