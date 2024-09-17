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
    public class _05CustomerOverdueAmount
    {
        [Fact]
        public async Task Test()
        {
            var expectedStatusCode = System.Net.HttpStatusCode.BadRequest;

            var stopwatch = Stopwatch.StartNew();
            var httpClient = Helper.CreateHttpClient();
            var request = new CreditApplicationCreateDto()
            {
                Amount = 2500,
                DurationMonths = 36,
                NetMonthlyIncome = 2500,
                NIF = "765434223"
            };

            // Act.
            var response = await httpClient.PostAsJsonAsync($"/{Helper.CreditApplicationServiceCreateUri}", request);
            // Assert.
            Helper.AssertCommonResponseParts(stopwatch, response, expectedStatusCode);
        }
    }
}
