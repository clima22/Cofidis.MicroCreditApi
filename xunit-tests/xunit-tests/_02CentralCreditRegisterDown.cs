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
    public class _02CentralCreditRegisterDown
    {
        /// <summary>
        /// Simular serviço CentralCreditRegister não response (enviar um NIF que não está na lista de configuração da api)
        /// </summary>
        /// <returns></returns>
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
                DurationMonths = 24,
                NetMonthlyIncome = 1500,
                NIF = "456162333"
            };

            // Act.
            var response = await httpClient.PostAsJsonAsync($"/{Helper.CreditApplicationServiceCreateUri}", request);
            // Assert.
            Helper.AssertCommonResponseParts(stopwatch, response, expectedStatusCode);
        }
    }
}
