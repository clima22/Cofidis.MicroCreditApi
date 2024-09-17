namespace Cofidis.MicroCreditApi.Infra.ExternalApi.CentralCreditRegister
{
    public interface ICentralCreditRegisterService
    {
        /// <summary>
        /// Devolve a informação de responsabilidade de créditos de um cliente
        /// </summary>
        /// <param name="NIF"></param>
        /// <returns></returns>
        bool GetMap(string NIF, out CreditRegisterMapDto map);
    }
}
