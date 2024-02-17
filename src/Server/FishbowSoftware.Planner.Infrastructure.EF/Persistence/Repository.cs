using FishbowSoftware.Planner.Domain.Core;
using FishbowSoftware.Planner.Domain.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FishbowSoftware.Planner.Infrastructure.Persistence
{
    internal class Repository<TDbContext, TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity<string>
        where TDbContext : DbContext
    {
        private readonly TDbContext _dbContext;

        public Repository(TDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specification)
        {
            return SpecificationEvaluator.BuildQuery(_dbContext.Set<TEntity>(), specification);
        }

        public IQueryable<TEntity> Query(params string[] includes)
        {
            return ApplyIncludes(_dbContext.Set<TEntity>(), includes);
        }

        public Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = default)
        {
            return predicate is null ? _dbContext.Set<TEntity>().CountAsync() : _dbContext.Set<TEntity>().CountAsync(predicate);
        }

        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().AnyAsync(predicate);
        }

        public Task<TEntity?> GetAsync(
            Expression<Func<TEntity, bool>> predicate,
            bool disableChangeTracking = true,
            params string[] includes)
        {
            var query = CreateQuery(disableChangeTracking, includes);
            return query.FirstOrDefaultAsync(predicate);
        }

        public Task<List<TEntity>> GetListAsync(
            Expression<Func<TEntity, bool>>? predicate = default,
            bool disableChangeTracking = true,
            params string[] includes)
        {
            var query = CreateQuery(disableChangeTracking, includes);

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return query.ToListAsync();
        }

        public Task<List<TEntity>> GetListAsync(
            ISpecification<TEntity>? specification = default,
            bool disableChangeTracking = true)
        {
            var query = specification is null
                ? _dbContext.Set<TEntity>()
                : SpecificationEvaluator.BuildQuery(_dbContext.Set<TEntity>(), specification);

            if (disableChangeTracking)
            {
                query = query.AsNoTracking();
            }

            return query.ToListAsync();
        }

        public Task AddAsync(TEntity entity)
        {
            return _dbContext.Set<TEntity>().AddAsync(entity).AsTask();
        }

        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
        }

        public void Delete(TEntity? entity)
        {
            if (entity is null)
            {
                return;
            }

            _dbContext.Set<TEntity>().Remove(entity);
        }


        public Task<int> BatchDeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().Where(predicate).DeleteFromQueryAsync();
        }

        private IQueryable<TEntity> CreateQuery(bool disableChangeTracking, params string[] includes)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();
            query = ApplyIncludes(query, includes);

            if (disableChangeTracking)
            {
                query = query.AsNoTracking();
            }

            return query;
        }

        private static IQueryable<TEntity> ApplyIncludes(IQueryable<TEntity> query, params string[] includes)
        {
            return includes.Aggregate(query, (current, propertyPath) => current.Include(propertyPath));
        }
    }
}
