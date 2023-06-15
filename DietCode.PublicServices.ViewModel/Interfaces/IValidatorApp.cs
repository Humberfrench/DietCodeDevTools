
using Dietcode.Api.Core.Results;

namespace DietCode.PublicServices.ViewModel.Interfaces
{
    public interface IValidatorApp
    {
        Task<MethodResult> ValidarCpf(string cpf);
        Task<MethodResult> ValidarCnpj(string cnpj);
        Task<MethodResult> ValidaBandeira(string card);
        Task<MethodResult> IsValidCreditCardNumber(string card);
    }
}
