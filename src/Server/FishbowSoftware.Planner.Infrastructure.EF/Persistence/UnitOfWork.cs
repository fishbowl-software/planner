using FishbowSoftware.Planner.Domain.Core;
using FishbowSoftware.Planner.Domain.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace FishbowSoftware.Planner.Infrastructure.Persistence
{
    internal class UnitOfWork<TDbContext> : IUnitOfWork where TDbContext : DbContext
    {
        private readonly TDbContext _dbContext;
        private readonly Hashtable _repositories = new();

        public UnitOfWork(TDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class, IEntity<string>
        {
            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryInstance = new Repository<TDbContext, TEntity>(_dbContext);
                _repositories.Add(type, repositoryInstance);
            }

            if (_repositories[type] is not Repository<TDbContext, TEntity> repository)
            {
                throw new InvalidOperationException("Could not create a repository");
            }

            return repository;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            _dbContext.ExecuteFutureAction();
            var changes = await _dbContext.SaveChangesAsync(cancellationToken);
            return changes;
        }
    }
}
