using System;
using System.Net;

namespace ApiTester.Models
{
    public class ApiResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Body { get; set; } = string.Empty;
        public string Headers { get; set; } = string.Empty;
        public TimeSpan Duration { get; set; }
        public long ContentLength { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
