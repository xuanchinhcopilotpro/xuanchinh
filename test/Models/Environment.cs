using System;
using System.Collections.Generic;

namespace ApiTester.Models
{
    public class Environment
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = "New Environment";
        public Dictionary<string, string> Variables { get; set; } = new();
        public bool IsActive { get; set; }
    }
}
