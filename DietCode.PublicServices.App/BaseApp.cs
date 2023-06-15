using Dietcode.Api.Core.Results;
using Dietcode.Core.DomainValidator;
using DietCode.PublicServices.Domain.Interfaces.Repository;

namespace FluxoDeCaixa.App
{
    public class BaseApp<T> : AppServiceBase where T : new()
    {
        private readonly IUnitOfWork uow;
        protected ValidationResult<T> baseValidationResult;

        public BaseApp(IUnitOfWork uow)
        {
            this.uow = uow;
            baseValidationResult = new ValidationResult<T>();
        }

        public void BeginTransaction()
        {
            uow.BeginTransaction();
        }

        public async Task CommitAync()
        {
            var retorno = await uow.SaveChangesAsync();

            if (!retorno.Valid)
            {
                retorno.Erros.ToList().ForEach(e => baseValidationResult.Add(e.Message));
            }
        }
        public void Commit()
        {
            var retorno = uow.SaveChanges();

            if (!retorno.Valid)
            {
                retorno.Erros.ToList().ForEach(e => baseValidationResult.Add(e.Message));
            }
        }

        protected ErrorValidation ConvertValidationErrors(List<ValidationError> erros)
        {
            string erro;
            if(erros.Count == 1)
            {
                erro = erros.First().Message;
            }
            else
            {
                erro = string.Join(" *** ", erros);
            }

            return new ErrorValidation
            {
                Code = "10000",
                Message = erro
            };

        }
    }
}