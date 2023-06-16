using Dietcode.Api.Core.Results;

namespace DietCode.PublicServices.ViewModel.Interfaces
{
    public interface ICardApp
    {
        Task<MethodResult> ValidaBandeira(string card);
        Task<MethodResult> IsValidCreditCardNumber(string card);
    }
}
