using GitHub_KPI_tool_Application.Abstraction.Repositories;
using GitHub_KPI_tool_Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace GitHub_KPI_tool_Infrastructure.Repositories;

public abstract class BaseRepository<TEntity>(DbContext dbContext) : IBaseRepository<TEntity>
    where TEntity : BaseEntity
{
    private readonly DbSet<TEntity> _dbSet = dbContext.Set<TEntity>();
    private readonly DbContext _dbContext = dbContext;

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<TEntity> InsertAsync(TEntity entity)
    {
        var result = await _dbSet.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return result.Entity;
    }

    public async Task Attach(TEntity entity)
    {
        _dbSet.Attach(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task AttachRange(IEnumerable<TEntity> entities)
    {
        _dbSet.AttachRange(entities);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteRange(IEnumerable<TEntity> entities)
    {
        _dbSet.RemoveRange(entities);
        await _dbContext.SaveChangesAsync();
    }
}