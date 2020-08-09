using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApp.BusinessEntity.Model;
using ToDoApp.BusinessLogic.Service.Interface;

namespace ToDoApp.BusinessLogic.Service
{
    /// <summary>
    /// The todo task service.
    /// </summary>
    public class ToDoTaskService : BaseService<ToDoTask>, IToDoTaskService
    {
        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="repository">The todo task repository.</param>
        public ToDoTaskService(IRepository<ToDoTask> repository)
            : base(repository)
        {

        }

        /// <summary>
        /// Queries todos by query type.
        /// </summary>
        /// <param name="listQueryType">The query type.</param>
        /// <returns></returns>
        public async Task<IEnumerable<ToDoTask>> QueryByTypeAsync(ListQueryType listQueryType)
        {
            var result = new List<ToDoTask>();

            switch (listQueryType)
            {
                case ListQueryType.All:
                {
                    result = await this.GetByAsync(x => !x.IsDeleted).ConfigureAwait(false);
                    break;
                }
                case ListQueryType.InProgress:
                {
                    result = await this.GetByAsync(x => !x.IsDeleted && !x.IsDone).ConfigureAwait(false);
                    break;
                }
                case ListQueryType.Done:
                {
                    result = await this.GetByAsync(x => !x.IsDeleted && x.IsDone).ConfigureAwait(false);
                    break;
                }
                default:
                {
                    result = await this.GetByAsync(x => !x.IsDeleted).ConfigureAwait(false);
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Set todo task done.
        /// </summary>
        /// <param name="id">The todo id.</param>
        /// <returns></returns>
        public async Task<ToDoTask> SetTodoTaskDoneAsync(int id)
        {
            var entity = await this.GetByIdAsync(id).ConfigureAwait(false);

            if (entity == null)
            {
                throw new NullReferenceException(nameof(entity));
            }

            entity.SetDone();

            return await this.UpdateAsync(entity).ConfigureAwait(false);
        }
    }
}
