using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DietCode.PublicServices.Domain.Interfaces.Services
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        Task<bool> Adicionar(TEntity obj);
        Task<bool> Atualizar(TEntity obj);
        Task<bool> Remover(TEntity obj);
        Task<bool> Remover(int id);
        Task<TEntity> ObterPorId(int id);
        Task<IEnumerable<TEntity>> ObterTodos();
        Task<IEnumerable<TEntity>> Pesquisar(Expression<Func<TEntity, bool>> predicate);
    }
}
