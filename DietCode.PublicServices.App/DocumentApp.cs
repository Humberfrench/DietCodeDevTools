using Dietcode.Api.Core.Results;
using DietCode.PublicServices.Domain.Interfaces.Services;
using DietCode.PublicServices.ViewModel.Interfaces;

namespace DietCode.PublicServices.App
{
    public class DocumentApp : AppServiceBase, IDocumentApp
    {
        private readonly IDocumentService service;

        public DocumentApp(IDocumentService service) : base()
        {
            this.service = service;
        }

        public async Task<MethodResult> GerarCPF(bool formatado = false)
        {
            var serviceResult = await service.GerarCPF(formatado);
            return Ok(serviceResult);
        }

        public async Task<MethodResult> GerarCNPJ(bool formatado = false)
        {
            var serviceResult = await service.GerarCNPJ(formatado);
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
