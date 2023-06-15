using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DietCode.PublicServices.Domain.Interfaces.Repository
{
    public interface IBaseRepository<TEntity> : IDisposable
    {
        Task<bool> Adicionar(TEntity entity);
        Task<bool> AdicionarRange(IEnumerable<TEntity> entities);
        Task<bool> Atualizar(TEntity entity);
        Task<int> CountAsync();
        Task<TEntity> ObterPorId(int id);
        Task<IEnumerable<TEntity>> ObterTodos();
        Task<IEnumerable<TEntity>> ObterTodosPaginado(int pagina, int registros);
        Task<IEnumerable<TEntity>> Pesquisar(Expression<Func<TEntity, bool>> predicate);
        Task<bool> Remover(TEntity entity);
        Task RemoveRange(IEnumerable<TEntity> entities);
    }
}
