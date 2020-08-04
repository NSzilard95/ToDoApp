using System;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using ToDoApp.BusinessEntity.Model.Base;
using ToDoApp.BusinessLogic.Service.Interface;

namespace ToDoApp.BusinessLogic.Service
{
    /// <summary>
    /// The base service abstract class.
    /// </summary>
    /// <typeparam name="TEntity">The entity.</typeparam>
    public class BaseService<TEntity> : IService<TEntity>
        where TEntity : BaseEntity
    {
        /// <summary>
        /// The repository.
        /// </summary>
        private readonly IRepository<TEntity> repository;

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public BaseService(IRepository<TEntity> repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Get all entities.
        /// </summary>
        /// <returns>List of entities.</returns>
        public async Task<List<TEntity>> GetAllAsync()
        {
            return await repository.GetAllAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Get entity by id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Entity.</returns>
        public async Task<TEntity> GetByIdAsync(long id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id));
            }

            return await repository.GetByIdAsync(id).ConfigureAwait(false);
        }

        /// <summary>
        /// Get entities by predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>List of entities.</returns>
        public async Task<List<TEntity>> GetByAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await repository.GetByAsync(predicate).ConfigureAwait(false);
        }

        /// <summary>
        /// Add new entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>New entity.</returns>
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new NullReferenceException(nameof(entity));
            }

            return await repository.AddAsync(entity).ConfigureAwait(false);
        }

        /// <summary>
        /// Update entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Updated entity.</returns>
        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new NullReferenceException(nameof(entity));
            }

            return await repository.UpdateAsync(entity).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete entity by id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Deleted entity.</returns>
        public async Task<TEntity> DeleteAsync(long id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id));
            }

            var entity = await repository.GetByIdAsync(id).ConfigureAwait(false);

            if (entity == null)
            {
                throw new NullReferenceException(nameof(entity));
            }

            entity.Delete();

            return await repository.UpdateAsync(entity).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete entity physically by id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Deleted entity.</returns>
        public async Task<TEntity> DeletePhysicalAsync(long id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id));
            }

            return await repository.DeletePhysicalAsync(id).ConfigureAwait(false);
        }
    }
}
