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
    public class _03DurationContractConstraint
    {
        [Fact]
        public async Task MaxAge()
        {
            var expectedStatusCode = System.Net.HttpStatusCode.BadRequest;

            var stopwatch = Stopwatch.StartNew();
            var httpClient = Helper.CreateHttpClient();
            var request = new CreditApplicationCreateDto()
            {
                Amount = 1000,
                DurationMonths = 6,
                NetMonthlyIncome = 1500,
                NIF = "987654321"
            };

            // Act.
            var response = await httpClient.PostAsJsonAsync($"/{Helper.CreditApplicationServiceCreateUri}", request);
            // Assert.
            Helper.AssertCommonResponseParts(stopwatch, response, expectedStatusCode);
        }

        [Fact]
        public async Task MinDuration()
        {
            var expectedStatusCode = System.Net.HttpStatusCode.BadRequest;

            var stopwatch = Stopwatch.StartNew();
            var httpClient = Helper.CreateHttpClient();
            var request = new CreditApplicationCreateDto()
            {
                Amount = 1000,
                DurationMonths = 1,
                NetMonthlyIncome = 1500,
                NIF = "123456789"
            };

            // Act.
            var response = await httpClient.PostAsJsonAsync($"/{Helper.CreditApplicationServiceCreateUri}", request);
            // Assert.
            Helper.AssertCommonResponseParts(stopwatch, response, expectedStatusCode);
        }

        [Fact]
        public async Task MaxDuration()
        {
            var expectedStatusCode = System.Net.HttpStatusCode.BadRequest;

            var stopwatch = Stopwatch.StartNew();
            var httpClient = Helper.CreateHttpClient();
            var request = new CreditApplicationCreateDto()
            {
                Amount = 1000,
                DurationMonths = 100,
                NetMonthlyIncome = 1500,
                NIF = "123456789"
            };

            // Act.
            var response = await httpClient.PostAsJsonAsync($"/{Helper.CreditApplicationServiceCreateUri}", request);
            // Assert.
            Helper.AssertCommonResponseParts(stopwatch, response, expectedStatusCode);
        }
    }
}
