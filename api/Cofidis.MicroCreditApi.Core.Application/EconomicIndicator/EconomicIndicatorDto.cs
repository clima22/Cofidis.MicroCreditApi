using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofidis.MicroCreditApi.Core.Application.EconomicIndicator
{
    public class EconomicIndicatorDto
    {
        /// <summary>
        /// Taxa de inflação em percentagem
        /// </summary>
        public decimal InflationRate { get; set; } = (decimal)2.8;
        /// <summary>
        /// Taxa de desemprego em percentagem
        /// </summary>
        public decimal UnemploymentRate { get; set; } = (decimal)6.5;
    }
}
