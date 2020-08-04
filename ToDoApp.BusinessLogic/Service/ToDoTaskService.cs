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
        /// The todo task repository.
        /// </summary>
        private readonly IRepository<ToDoTask> repository;

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="repository">The todo task repository.</param>
        public ToDoTaskService(IRepository<ToDoTask> repository)
            : base(repository)
        {
            this.repository = repository;
        }
    }
}
