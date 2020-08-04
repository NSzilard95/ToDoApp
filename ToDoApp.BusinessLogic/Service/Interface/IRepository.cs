using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ToDoApp.BusinessEntity.Model.Base;

namespace ToDoApp.BusinessLogic.Service.Interface
{
    /// <summary>
    /// The base repository interface.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Get all entities.
        /// </summary>
        /// <returns>List of entities.</returns>
        Task<List<T>> GetAllAsync();

        /// <summary>
        /// Get entities by predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>List of entities.</returns>
        Task<List<T>> GetByAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Get entity by id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Entity.</returns>
        Task<T> GetByIdAsync(long id);

        /// <summary>
        /// Add new entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>New entity.</returns>
        Task<T> AddAsync(T entity);

        /// <summary>
        /// Update entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Updated entity.</returns>
        Task<T> UpdateAsync(T entity);

        /// <summary>
        /// Delete entity by id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Deleted entity.</returns>
        Task<T> DeletePhysicalAsync(long id);
    }
}
