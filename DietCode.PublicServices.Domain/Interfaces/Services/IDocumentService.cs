using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DietCode.PublicServices.Domain.Interfaces.Services
{
    public interface IDocumentService
    {
        Task<string> GerarCPF(bool formatado = false);
        Task<string> GerarCNPJ(bool formatado=false);
        Task<bool> ValidarCpf(string cpf);
        Task<bool> ValidarCnpj(string cnpj);
    }
}
