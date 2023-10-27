using Revision.Domain.Commons;
using System.Linq.Expressions;

namespace Revision.DataAccess.IRepositories;

public interface IRepository<TEntity> where TEntity : Auditable
{
    Task<TEntity> AddAsync(TEntity entity);
    TEntity Update(TEntity entity);
    void Delete(TEntity entity);
    void Destroy(TEntity entity);
    Task<TEntity> SelectAsync(Expression<Func<TEntity, bool>> expression, string[] includes = null);
    IQueryable<TEntity> Select(Expression<Func<TEntity, bool>> expression = null,
        bool isTracking = false,
        string[] includes = null);
    Task<bool> SaveAsync();
}