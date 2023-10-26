using Revision.Domain.Commons;
using System.Linq.Expressions;
using Revision.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using Revision.DataAccess.IRepositories;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Revision.DataAccess.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Auditable
{
    private readonly DbSet<TEntity> _dbSet;
    private readonly RevisionDbContext _dbContext;
    public Repository(RevisionDbContext dbContext, DbSet<TEntity> dbSet)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<TEntity>();
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        EntityEntry<TEntity> entry = await _dbSet.AddAsync(entity);

        return entry.Entity;
    }

    public TEntity Update(TEntity entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        EntityEntry<TEntity> entry = _dbContext.Update(entity);

        return entry.Entity;
    }

    public void Delete(TEntity entity)
    {
        entity.IsDeleted = true;
    }

    public void Destroy(TEntity entity)
    {
        _dbContext.Remove(entity);
    }

    public async Task<TEntity> SelectAsync(Expression<Func<TEntity, bool>> expression, string[] includes = null)
    {
        IQueryable<TEntity> entities = expression == null ? _dbSet.AsQueryable() : _dbSet.Where(expression).AsQueryable();

        if (includes is not null)
            foreach (var include in includes)
                entities = entities.Include(include);

        return await entities.FirstOrDefaultAsync();
    }

    public IQueryable<TEntity> Select(Expression<Func<TEntity, bool>> expression = null, bool isTracking = false, string[] includes = null)
    {
        IQueryable<TEntity> entities = expression == null ? _dbSet.AsQueryable()
            : _dbSet.Where(expression).AsQueryable();

        entities = isTracking ? entities.AsNoTracking() : entities;

        if (includes is not null)
            foreach (var include in includes)
                entities = entities.Include(include);

        return entities;
    }

    public async Task<bool> SaveAsync()
         => await _dbContext.SaveChangesAsync() >= 0;
}