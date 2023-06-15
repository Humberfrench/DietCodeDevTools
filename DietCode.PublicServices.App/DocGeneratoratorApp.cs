using Dietcode.Api.Core.Results;
using DietCode.PublicServices.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DietCode.PublicServices.App
{
    public class DocGeneratoratorApp : AppServiceBase, IDocGeneratoratorApp
    {
        private readonly IDocGeneratoratorService service;
        
        public DocGeneratoratorApp(IDocGeneratoratorService service) : base()
        {
            this.service = service;
        }

        public async Task<MethodResult> GerarCPF()
        {
            var serviceResult = await service.GerarCPF();
            return Ok(serviceResult);
        }

        public async Task<MethodResult> GerarCNPJ()
        {
            var serviceResult = await service.GerarCNPJ();
            return Ok(serviceResult);
        }
    }
}
