using LeaderboardService.Domain;

namespace LeaderboardService.Application.Interfaces;

using System;
using System.Linq.Expressions;

public interface IRepository<TEntity> where TEntity : Entity
{
    Task<TEntity?> GetById(int id);
    
    Task<TEntity[]> GetAll();
    Task<TEntity[]> GetAll(int page, int pageSize);
    
    Task<TEntity[]> Find(Expression<Func<TEntity, bool>> predicate);
    
    Task AddOrUpdate(TEntity entity);
    
    Task Remove(TEntity entity);
    
    Task<int> Count(Expression<Func<TEntity, bool>> predicate);
}
