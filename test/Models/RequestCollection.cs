using System;
using System.Collections.Generic;

namespace ApiTester.Models
{
    public class RequestCollection
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = "New Collection";
        public List<ApiRequest> Requests { get; set; } = new();
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
