using System.Linq.Expressions;
using LeaderboardService.Application.Interfaces;
using LeaderboardService.Domain;
using Microsoft.EntityFrameworkCore;

namespace LeaderboardService.MongoDB.DataProvider;

public class MongoDbRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
{
    private readonly IDbContextFactory<MongoDbContext> _dbContextFactory;

    public MongoDbRepository(IDbContextFactory<MongoDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }
    
    public async Task<TEntity?> GetById(int id)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync();

        return await dbContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<TEntity[]> GetAll()
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync();

        return await dbContext.Set<TEntity>().AsNoTracking().ToArrayAsync();
    }

    public async Task<TEntity[]> GetAll(int page, int pageSize)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync();
        
        return await dbContext.Set<TEntity>()
            .AsNoTracking()
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToArrayAsync();
    }

    public async Task<TEntity[]> Find(Expression<Func<TEntity, bool>> predicate)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync();

        return await dbContext.Set<TEntity>().Where(predicate).AsNoTracking().ToArrayAsync();
    }

    public async Task AddOrUpdate(TEntity entity)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync();
       
        if (dbContext.Entry(entity).State == EntityState.Detached)
            dbContext.Set<TEntity>().Add(entity);
        
        await dbContext.SaveChangesAsync(); 
    }
    
    public async Task Remove(TEntity entity)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync();
       
        if (dbContext.Entry(entity).State != EntityState.Detached)
            dbContext.Set<TEntity>().Remove(entity);
        
        await dbContext.SaveChangesAsync();
    }

    public async Task<int> Count(Expression<Func<TEntity, bool>> predicate)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync();
        
        return await dbContext.Set<TEntity>().AsNoTracking().Where(predicate).CountAsync();
    }
}