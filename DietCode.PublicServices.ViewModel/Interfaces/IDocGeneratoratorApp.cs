using Dietcode.Api.Core.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DietCode.PublicServices.Domain.Interfaces.Services
{
    public interface IDocGeneratoratorApp
    {
        Task<MethodResult> GerarCPF();
        Task<MethodResult> GerarCNPJ();
    }
}
