using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ApiTester.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ApiTester.Services
{
    public class HttpService
    {
        private readonly HttpClient _httpClient;

        public HttpService()
        {
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
        }

        public async Task<ApiResponse> SendRequestAsync(ApiRequest request)
        {
            var stopwatch = Stopwatch.StartNew();
            var response = new ApiResponse();

            try
            {
                var httpRequest = new HttpRequestMessage(request.Method, request.Url);

                // Add headers
                foreach (var header in request.Headers)
                {
                    try
                    {
                        if (header.Key.Equals("Content-Type", StringComparison.OrdinalIgnoreCase))
                        {
                            continue; // Will be set with content
                        }
                        httpRequest.Headers.TryAddWithoutValidation(header.Key, header.Value);
                    }
                    catch { }
                }

                // Add body for POST, PUT, PATCH
                if (!string.IsNullOrWhiteSpace(request.Body) && 
                    (request.Method == HttpMethod.Post || request.Method == HttpMethod.Put || 
                     request.Method.Method == "PATCH"))
                {
                    var contentType = request.Headers.ContainsKey("Content-Type") 
                        ? request.Headers["Content-Type"] 
                        : "application/json";
                    httpRequest.Content = new StringContent(request.Body, Encoding.UTF8, contentType);
                }

                // Send request
                var httpResponse = await _httpClient.SendAsync(httpRequest);
                stopwatch.Stop();

                // Read response
                var responseBody = await httpResponse.Content.ReadAsStringAsync();
                
                // Format JSON if possible
                if (httpResponse.Content.Headers.ContentType?.MediaType?.Contains("json") == true)
                {
                    try
                    {
                        var jsonObj = JToken.Parse(responseBody);
                        responseBody = jsonObj.ToString(Formatting.Indented);
                    }
                    catch { }
                }

                response.StatusCode = httpResponse.StatusCode;
                response.Body = responseBody;
                response.IsSuccess = httpResponse.IsSuccessStatusCode;
                response.Duration = stopwatch.Elapsed;
                response.ContentLength = responseBody.Length;
                response.Headers = string.Join("\n", httpResponse.Headers.Select(h => $"{h.Key}: {string.Join(", ", h.Value)}"));
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                response.IsSuccess = false;
                response.ErrorMessage = ex.Message;
                response.Body = $"Error: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}";
                response.Duration = stopwatch.Elapsed;
            }

            return response;
        }
    }
}
