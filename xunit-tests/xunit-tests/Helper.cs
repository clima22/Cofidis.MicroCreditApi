using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace xunit_tests
{
    public class Helper
    {
        internal const string JsonMediaType = "application/json";
        internal const int ExpectedMaxElapsedMilliseconds = 5000;
        internal static JsonSerializerOptions JsonSerializerOptions = new() { PropertyNameCaseInsensitive = true };

        internal const string CreditApplicationBaseAddress = "https://localhost:7272/";
        internal const string CreditApplicationServiceCreateUri = "CreditApplication";
        
        public static HttpClient CreateHttpClient()
        {
            HttpClient httpClient = new() { BaseAddress = new Uri(CreditApplicationBaseAddress) };
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //httpClient.Timeout = TimeSpan.FromMilliseconds(ExpectedMaxElapsedMilliseconds);
            return httpClient;
        }

        public static async Task AssertResponseWithContentAsync<T>(Stopwatch stopwatch,
                HttpResponseMessage response,
                System.Net.HttpStatusCode expectedStatusCode,
                T expectedContent)
        {
            AssertCommonResponseParts(stopwatch, response, expectedStatusCode);
            Assert.Equal(JsonMediaType, response.Content.Headers.ContentType?.MediaType);
            Assert.Equal(expectedContent, await JsonSerializer.DeserializeAsync<T?>(
                await response.Content.ReadAsStreamAsync(), JsonSerializerOptions));
        }

        public static void AssertCommonResponseParts(Stopwatch stopwatch,
           HttpResponseMessage response, 
           System.Net.HttpStatusCode expectedStatusCode,
           int expectedMaxElapsedMilliseconds = ExpectedMaxElapsedMilliseconds)
        {
            Assert.Equal(expectedStatusCode, response.StatusCode);
            if (expectedMaxElapsedMilliseconds > 0)  Assert.True(stopwatch.ElapsedMilliseconds < expectedMaxElapsedMilliseconds);
        }
    }
}
