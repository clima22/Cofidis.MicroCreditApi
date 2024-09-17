using AutoMapper;
using Cofidis.MicroCreditApi.Core.Domain.Entities;
using Cofidis.MicroCreditApi.Infra.ExternalApi.DigitalMobileKey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofidis.MicroCreditApi.Core.Application.CreditApplication
{
    public class CreditApplicationMapper : Profile
    {
        public CreditApplicationMapper()
        {
            CreateMap<CustomerInfoDto, Customer>();
            CreateMap<CreditApplicationCreateDto, Domain.Entities.CreditApplication>();
        }
    }
}
