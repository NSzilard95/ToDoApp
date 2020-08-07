using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApp.BusinessEntity.Model;

namespace ToDoApp.BusinessLogic.Service.Interface
{
    /// <summary>
    /// The todo task service iterface.
    /// </summary>
    public interface IToDoTaskService : IService<ToDoTask>
    {
        /// <summary>
        /// Queries todos by query type.
        /// </summary>
        /// <param name="listQueryType">The query type.</param>
        /// <returns></returns>
        Task<IEnumerable<ToDoTask>> QueryByType(ListQueryType listQueryType);
    }
}
