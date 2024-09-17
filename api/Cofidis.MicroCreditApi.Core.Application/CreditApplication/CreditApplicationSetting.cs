namespace Cofidis.MicroCreditApi.Core.Application.CreditApplication
{
    internal class CreditApplicationSetting
    {
        /// <summary>
        /// Taxa de juro anual a aplicar
        /// </summary>
        public decimal AnnualInterestRate { get; set; } = (decimal)5.0;
        /// <summary>
        /// Coeficiente a ponderar no calculo de indice de risco
        /// </summary>
        public RiskIndexCoefficientSetting RiskIndexCoefficient { get; set; } = new RiskIndexCoefficientSetting();
        /// <summary>
        /// Valor máximo do índice de risco permitido para aprovação do crédito 
        /// </summary>
        public decimal MaxRiskIndexAllowed { get; set; } = (decimal)0.28;
    }

    internal class RiskIndexCoefficientSetting
    {
        /// <summary>
        /// Coeficiente para ponderação da taxa de inflação  
        /// </summary>
        public decimal InflationRate { get; set; } = (decimal)0.1;
        /// <summary>
        /// Coeficiente para ponderação da taxa de desemprego  
        /// </summary>
        public decimal UnemploymentRate { get; set; } = (decimal)0.2;
        /// <summary>
        /// Coeficiente para ponderação da taxa de esforço  
        /// </summary>
        public decimal EffortRate { get; set; } = (decimal)0.7;
    }
}
