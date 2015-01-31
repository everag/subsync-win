using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SubSync.Lib
{
    [Serializable]
    public class ApiRequestException : Exception
    {
        public ApiRequestException(string message, Exception e) : base(message, e)
        {
        }

        public ApiRequestException(string message, HttpStatusCode responseCode, Exception e) : base(message, e)
        {
            ResponseCode = responseCode;
        }

        public HttpStatusCode ResponseCode { get; set; }
    }
}
