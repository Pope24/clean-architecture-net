using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Bases
{
    public class BaseResponse<T>
    {
        public BaseResponse()
        {

        }
        public BaseResponse(T data, string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }
        public BaseResponse(string message)
        {
            Succeeded = false;
            Message = message;
        }
        public BaseResponse(string message, bool succeeded)
        {
            Succeeded = succeeded;
            Message = message;
        }

        public HttpStatusCode StatusCode { get; set; }

        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public Dictionary<string, string> Errors { get; set; } = new Dictionary<string, string>();
        public T Data { get; set; }
    }
}
