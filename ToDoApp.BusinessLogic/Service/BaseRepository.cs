using System;
using System.Linq;
using ToDoApp.Data;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ToDoApp.BusinessEntity.Model.Base;
using ToDoApp.BusinessLogic.Service.Interface;

namespace ToDoApp.BusinessLogic.Service
{
    /// <summary>
    /// The base repository abstract class.
    /// </summary>
    /// <typeparam name="TEntity">The entity.</typeparam>
    public class BaseRepository<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity
    {
        /// <summary>
        /// The db context.
        /// </summary>
        private readonly ToDoAppDataBase context;

        /// <summary>
        /// The entity db set.
        /// </summary>
        public DbSet<TEntity> DbSet;

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="context">The db context.</param>
        public BaseRepository(ToDoAppDataBase context)
        {
            this.context = context ?? throw new NullReferenceException(nameof(context));
            this.DbSet = context.Set<TEntity>();
        }

        /// <summary>
        /// Get entity by id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Entity.</returns>
        public async Task<TEntity> GetByIdAsync(long id)
        {
            return await DbSet.FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// Get all entities.
        /// </summary>
        /// <returns>List of entities.</returns>
        public async Task<List<TEntity>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        /// <summary>
        /// Get entities by predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>List of entities.</returns>
        public async Task<List<TEntity>> GetByAsync(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
            {
                predicate = (x) => true;
            }

            return await DbSet.Where(predicate).ToListAsync();
        }

        /// <summary>
        /// Add new entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>New entity.</returns>
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            DbSet.Add(entity);
            await context.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }

        /// <summary>
        /// Update entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Updated entity.</returns>
        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }

        /// <summary>
        /// Delete entity by id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Deleted entity.</returns>
        public async Task<TEntity> DeletePhysicalAsync(long id)
        {
            var entity = await DbSet.FindAsync(id);
            if (entity == null)
            {
                return entity;
            }

            DbSet.Remove(entity);
            await context.SaveChangesAsync().ConfigureAwait(false);

            return entity;
        }
    }
}
