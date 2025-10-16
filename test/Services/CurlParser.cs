using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using ApiTester.Models;

namespace ApiTester.Services
{
    public class CurlParser
    {
        public ApiRequest ParseCurl(string curlCommand)
        {
            var request = new ApiRequest();

            // Remove "curl" keyword and extra whitespace
            curlCommand = curlCommand.Trim();
            if (curlCommand.StartsWith("curl", StringComparison.OrdinalIgnoreCase))
            {
                curlCommand = curlCommand.Substring(4).Trim();
            }

            // Extract URL - first quoted string or first argument
            var urlMatch = Regex.Match(curlCommand, @"['""](^['""]+)['""]]");
            if (urlMatch.Success)
            {
                request.Url = urlMatch.Groups[1].Value;
            }
            else
            {
                var parts = curlCommand.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length > 0)
                {
                    request.Url = parts[0];
                }
            }

            // Extract method (-X or --request)
            var methodMatch = Regex.Match(curlCommand, @"(?:-X|--request)\s+(\w+)", RegexOptions.IgnoreCase);
            if (methodMatch.Success)
            {
                request.Method = new HttpMethod(methodMatch.Groups[1].Value.ToUpper());
            }
            else
            {
                // Check for --data or --data-raw (implies POST)
                if (Regex.IsMatch(curlCommand, @"(?:--data|--data-raw|-d)\s+"))
                {
                    request.Method = HttpMethod.Post;
                }
                else
                {
                    request.Method = HttpMethod.Get;
                }
            }

            // Extract headers (-H or --header)
            var headerMatches = Regex.Matches(curlCommand, @"(?:-H|--header)\s+['""](^['""]+)['""]]", RegexOptions.IgnoreCase);
            foreach (Match match in headerMatches)
            {
                var headerValue = match.Groups[1].Value;
                var colonIndex = headerValue.IndexOf(':');
                if (colonIndex > 0)
                {
                    var key = headerValue.Substring(0, colonIndex).Trim();
                    var value = headerValue.Substring(colonIndex + 1).Trim();
                    request.Headers[key] = value;
                }
            }

            // Extract body (--data, --data-raw, -d)
            var dataMatch = Regex.Match(curlCommand, @"(?:--data-raw|--data|-d)\s+['""](+?)['""]]", RegexOptions.Singleline);
            if (dataMatch.Success)
            {
                request.Body = dataMatch.Groups[1].Value;
            }
            else
            {
                // Try without quotes
                dataMatch = Regex.Match(curlCommand, @"(?:--data-raw|--data|-d)\s+(\S+)");
                if (dataMatch.Success)
                {
                    request.Body = dataMatch.Groups[1].Value;
                }
            }

            request.Name = $"{request.Method} {request.Url}";
            return request;
        }
    }
}
