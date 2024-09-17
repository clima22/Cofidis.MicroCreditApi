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
    public class _04LimiteAmountConstraint
    {
        [Fact]
        public async Task MinAmount()
        {
            var expectedStatusCode = System.Net.HttpStatusCode.BadRequest;

            var stopwatch = Stopwatch.StartNew();
            var httpClient = Helper.CreateHttpClient();
            var request = new CreditApplicationCreateDto()
            {
                Amount = 50,
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
        public async Task MaxAmountInterval1000()
        {
            var expectedStatusCode = System.Net.HttpStatusCode.BadRequest;

            var stopwatch = Stopwatch.StartNew();
            var httpClient = Helper.CreateHttpClient();
            var request = new CreditApplicationCreateDto()
            {
                Amount = 1001,
                DurationMonths = 6,
                NetMonthlyIncome = 1000,
                NIF = "987654321"
            };

            // Act.
            var response = await httpClient.PostAsJsonAsync($"/{Helper.CreditApplicationServiceCreateUri}", request);
            // Assert.
            Helper.AssertCommonResponseParts(stopwatch, response, expectedStatusCode);
        }

        [Fact]
        public async Task MaxAmountInterval2000()
        {
            var expectedStatusCode = System.Net.HttpStatusCode.BadRequest;

            var stopwatch = Stopwatch.StartNew();
            var httpClient = Helper.CreateHttpClient();
            var request = new CreditApplicationCreateDto()
            {
                Amount = 2500,
                DurationMonths = 6,
                NetMonthlyIncome = 2000,
                NIF = "987654321"
            };

            // Act.
            var response = await httpClient.PostAsJsonAsync($"/{Helper.CreditApplicationServiceCreateUri}", request);
            // Assert.
            Helper.AssertCommonResponseParts(stopwatch, response, expectedStatusCode);
        }

        [Fact]
        public async Task MaxAmountInterval5000()
        {
            var expectedStatusCode = System.Net.HttpStatusCode.BadRequest;

            var stopwatch = Stopwatch.StartNew();
            var httpClient = Helper.CreateHttpClient();
            var request = new CreditApplicationCreateDto()
            {
                Amount = 6500,
                DurationMonths = 6,
                NetMonthlyIncome = 5500,
                NIF = "987654321"
            };

            // Act.
            var response = await httpClient.PostAsJsonAsync($"/{Helper.CreditApplicationServiceCreateUri}", request);
            // Assert.
            Helper.AssertCommonResponseParts(stopwatch, response, expectedStatusCode);
        }
    }
}
