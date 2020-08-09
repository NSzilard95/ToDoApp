using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.BusinessEntity.Model;
using ToDoApp.BusinessLogic.Service.Interface;

namespace ToDoApp.WebServer.Controllers
{
    /// <summary>
    /// The todo task API controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoTaskController : ControllerBase
    {
        /// <summary>
        /// The todo task service.
        /// </summary>
        private readonly IToDoTaskService toDoTaskService;

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="toDoTaskService">The todo task service.</param>
        public ToDoTaskController(IToDoTaskService toDoTaskService)
        {
            this.toDoTaskService = toDoTaskService;
        }

        /// <summary>
        /// Queries todos by query type.
        /// </summary>
        /// <param name="listQueryType">The query type.</param>
        /// <returns></returns>
        [HttpGet("GetForList/{listQueryType}")]
        public async Task<IActionResult> GetByTypeAsync(ListQueryType listQueryType)
        {
            try
            {
                var result = await toDoTaskService.QueryByTypeAsync(listQueryType).ConfigureAwait(false);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest("Error occured at data load.");
            }
        }

        /// <summary>
        /// The get by identifier API.
        /// </summary>
        /// <param name="id">The entity indentifier.</param>
        /// <returns>Entity.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            try
            {
                var result = await toDoTaskService.GetByIdAsync(id).ConfigureAwait(false);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest("Error occured at data load.");
            }
        }

        /// <summary>
        /// The post API.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <returns>Added entity.</returns>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] ToDoTask entity)
        {
            try
            {
                var addedEntity = await toDoTaskService.AddAsync(entity).ConfigureAwait(false);
                return Ok(addedEntity);
            }
            catch (Exception)
            {
                return BadRequest("Error occured at data saving.");
            }
        }

        /// <summary>
        /// The update API.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <returns>Updated entity.</returns>
        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] ToDoTask entity)
        {
            try
            {
                var updatedEntity = await toDoTaskService.UpdateAsync(entity).ConfigureAwait(false);
                return Ok(updatedEntity);
            }
            catch (Exception)
            {
                return BadRequest("Error occured at data saving.");
            }
        }

        /// <summary>
        /// Deletes entity logically.
        /// </summary>
        /// <param name="id">The entity indentifier.</param>
        /// <returns>Deleted entity.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            try
            {
                var deleted = await toDoTaskService.DeleteAsync(id).ConfigureAwait(false);
                return Ok(deleted);
            }
            catch (Exception)
            {
                return BadRequest("Error occured at deleting data.");
            }
        }

        /// <summary>
        /// Set todo task done.
        /// </summary>
        /// <param name="id">The todo id.</param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public async Task<IActionResult> SetDoneAsync(int id)
        {
            try
            {
                var updated = await toDoTaskService.SetTodoTaskDoneAsync(id).ConfigureAwait(false);
                return Ok(updated);
            }
            catch (Exception)
            {
                return BadRequest("Error occured at data updating.");
            }
        }

    }
}