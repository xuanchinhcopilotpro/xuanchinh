using System;
using System.Collections.Generic;
using System.Net.Http;

namespace ApiTester.Models
{
    public class ApiRequest
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public HttpMethod Method { get; set; } = HttpMethod.Get;
        public Dictionary<string, string> Headers { get; set; } = new();
        public string Body { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string CollectionId { get; set; } = string.Empty;
    }
}
