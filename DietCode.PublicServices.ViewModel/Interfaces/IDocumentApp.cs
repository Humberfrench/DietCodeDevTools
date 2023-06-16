using Dietcode.Api.Core.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DietCode.PublicServices.ViewModel.Interfaces
{
    public interface IDocumentApp
    {
        Task<MethodResult> GerarCPF(bool formatado = false);
        Task<MethodResult> GerarCNPJ(bool formatado = false);
        Task<MethodResult> ValidarCpf(string cpf);
        Task<MethodResult> ValidarCnpj(string cnpj);
    }
}
