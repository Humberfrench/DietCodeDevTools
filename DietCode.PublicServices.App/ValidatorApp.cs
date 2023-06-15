using Dietcode.Api.Core.Results;
using Dietcode.Core.DomainValidator;
using Dietcode.Core.Lib;
using DietCode.PublicServices.Domain.Interfaces.Services;
using DietCode.PublicServices.ViewModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluxoDeCaixa.App
{

    public class ValidatorApp : AppServiceBase, IValidatorApp
    {
        private readonly IValidatorService service;

        public ValidatorApp(IValidatorService service) : base()
        {
            this.service = service;
        }

        public async Task<MethodResult> IsValidCreditCardNumber(string card)
        {
            var serviceResult = await service.IsValidCreditCardNumber(card);
            return Ok(serviceResult);
        }

        public async Task<MethodResult> ValidaBandeira(string card)
        {
            var serviceResult = await service.ValidaBandeira(card);
            return Ok(serviceResult);

        }

        public async Task<MethodResult> ValidarCnpj(string cnpj)
        {
            var serviceResult = await service.ValidarCnpj(cnpj);
            return Ok(serviceResult);
        }

        public async Task<MethodResult> ValidarCpf(string cpf)
        {
            var serviceResult = await service.ValidarCpf(cpf);
            return Ok(serviceResult);
        }
    }
}
