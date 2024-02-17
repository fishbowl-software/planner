﻿using FishbowSoftware.Planner.Domain.Core;
using System.Linq.Expressions;

namespace FishbowSoftware.Planner.Domain.Persistence
{
    /// <summary>
    /// Generic repository.
    /// </summary>
    /// <typeparam name="TEntity">Class that implements the <see cref="IEntity{TKey}"/> interface</typeparam>
    public interface IRepository<TEntity> where TEntity : class, IEntity<string>
    {
        /// <summary>
        /// Applies the specified specification then returns an instance of the IQueryable
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specification);

        /// <summary>
        /// Gets the reference of the IQueryable instance
        /// </summary>
        /// <param name="includes">List of child entities to load</param>
        /// <returns></returns>
        IQueryable<TEntity> Query(params string[] includes);

        /// <summary>
        /// Counts number of entities.
        /// </summary>
        /// <param name="predicate">Predicate to filter query</param>
        /// <returns>Number of elements that satisfies the specified condition</returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = default);

        /// <summary>
        /// Checks whether the specified entity exists in the database that satisfies the condition.
        /// </summary>
        /// <param name="predicate">The predicate used to filter the query.</param>
        /// <returns>
        /// The task result contains a boolean value indicating whether any elements satisfy the specified condition.
        /// </returns>
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Gets an entity object filtered by predicate.
        /// </summary>
        /// <param name="predicate">Predicate to filter query</param>
        /// <param name="disableChangeTracking">Flag to specify if change tracking should be disabled (default: true)</param>
        /// <param name="includes">List of child entities to load</param>
        /// <returns>The entity object that satisfies the predicate, or null if not found</returns>
        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, bool disableChangeTracking = true, params string[] includes);

        /// <summary>
        /// Gets a list of the entity objects
        /// </summary>
        /// <param name="predicate">Predicate to filter query</param>
        /// <param name="disableChangeTracking">Flag to specify if change tracking should be disabled (default: true)</param>
        /// <param name="includes">List of child entities to load</param>
        /// <returns>List of entity objects</returns>
        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? predicate = default, bool disableChangeTracking = true, params string[] includes);

        /// <summary>
        /// Gets a list of the entity objects
        /// </summary>
        /// <param name="specification">Specification</param>
        /// <param name="disableChangeTracking">Flag to specify if change tracking should be disabled (default: true)</param>
        /// <returns>List of entity objects</returns>
        Task<List<TEntity>> GetListAsync(ISpecification<TEntity>? specification = default, bool disableChangeTracking = true);

        /// <summary>
        /// Adds new entry to database.
        /// </summary>
        /// <param name="entity">Entity object</param>
        Task AddAsync(TEntity entity);

        /// <summary>
        /// Updates existing entry.
        /// </summary>
        /// <param name="entity">Entity object</param>
        void Update(TEntity entity);

        /// <summary>
        /// Deletes entity object from database.
        /// </summary>
        /// <param name="entity">Entity object</param>
        void Delete(TEntity? entity);

        /// <summary>
        /// Deletes a collection of entities in bulk based on a specified condition.
        /// </summary>
        /// <param name="predicate">
        /// The expression specifying the condition for the entities to be deleted.
        /// </param>
        /// <remarks>
        /// This method provides a way to delete multiple entities that meet certain criteria
        /// in a single operation, offering a performance advantage over individual deletions.
        /// Use this method with caution, as it will delete all entities that match the condition.
        /// </remarks>
        /// <returns>A task with the number of rows affected.</returns>
        Task<int> BatchDeleteAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
