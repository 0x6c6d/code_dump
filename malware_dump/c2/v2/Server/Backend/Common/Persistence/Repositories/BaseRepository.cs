using Microsoft.EntityFrameworkCore;

namespace Backend.Common.Persistence.Repositories;

public class BaseRepository<TEntity>(DataContext dbContext) : IBaseRepository<TEntity>
    where TEntity : class
{
    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        var t = await dbContext.Set<TEntity>().FindAsync(id);
        return t;
    }

    public async Task<IReadOnlyList<TEntity>> GetAllAsync()
    {
        return await dbContext.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await dbContext.Set<TEntity>().AddAsync(entity);
        await dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task UpdateAsync(TEntity entity)
    {
        dbContext.Entry(entity).State = EntityState.Modified;
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(TEntity entity)
    {
        dbContext.Set<TEntity>().Remove(entity);
        await dbContext.SaveChangesAsync();
    }
}