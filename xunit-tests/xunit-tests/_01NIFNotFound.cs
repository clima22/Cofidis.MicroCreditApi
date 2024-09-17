using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using xunit_tests.DataObjects;

namespace xunit_tests
{
    public class _01NIFNotFound
    {
        /// <summary>
        /// Teste de NIF inexistente no CMD (Chave móvel digital)
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Test()
        {
            var expectedStatusCode = System.Net.HttpStatusCode.BadRequest;

            var stopwatch = Stopwatch.StartNew();
            var httpClient = Helper.CreateHttpClient();
            var request = new CreditApplicationCreateDto()
            {
                Amount = 1000,
                DurationMonths = 6,
                NetMonthlyIncome = 1500,
                NIF = "111111111"
            };

            // Act.
            var response = await httpClient.PostAsJsonAsync($"/{Helper.CreditApplicationServiceCreateUri}", request);
            // Assert.
            Helper.AssertCommonResponseParts(stopwatch, response, expectedStatusCode);
        }
    }
}