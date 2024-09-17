using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using xunit_tests.DataObjects;

namespace xunit_tests
{
    public class _07CustomerRiskIndexOK
    {
        [Fact]
        public async Task Test()
        {
            var expectedStatusCode = System.Net.HttpStatusCode.Created;

            var stopwatch = Stopwatch.StartNew();
            var httpClient = Helper.CreateHttpClient();
            var request = new CreditApplicationCreateDto()
            {
                Amount = 5000,
                DurationMonths = 24,
                NetMonthlyIncome = 3100,
                NIF = "123456789"
            };

            // Act.
            var response = await httpClient.PostAsJsonAsync($"/{Helper.CreditApplicationServiceCreateUri}", request);
            // Assert.
            Helper.AssertCommonResponseParts(stopwatch, response, expectedStatusCode);
        }
    }
}
