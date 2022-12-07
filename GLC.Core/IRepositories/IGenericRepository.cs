using System.Linq.Expressions;

namespace GLC.Core.IRepositories
{
  public interface IGenericRepository<TEntity, TEntityResource> where TEntity : class where TEntityResource : class
  {
    Task<TEntityResource> GetByIdAsync(Guid id);
    Task<IEnumerable<TEntityResource>> GetAllAsync();
    Task<TEntityResource> FindAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntityResource> AddAsync(TEntityResource entity);
    Task<TEntityResource> UpdateAsync(Guid id, TEntityResource entity);
    Task<TEntityResource> DeleteAsync(Guid id);

  }
}
