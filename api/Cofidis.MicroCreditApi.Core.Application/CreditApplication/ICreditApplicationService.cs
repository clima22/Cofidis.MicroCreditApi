using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofidis.MicroCreditApi.Core.Application.CreditApplication
{
    public interface ICreditApplicationService
    {
        /// <summary>
        /// Criar um pedido de crédito
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CreditApplicationCreateResponseDto Create(CreditApplicationCreateDto request);
    }
}
