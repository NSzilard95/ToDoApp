using Microsoft.EntityFrameworkCore;
using ToDoApp.BusinessEntity.Model;

namespace ToDoApp.Data
{
    /// <summary>
    /// The app database context
    /// </summary>
    public class ToDoAppDataBase : DbContext
    {
        /// <summary>
        /// The constructor
        /// </summary>
        /// <param name="options">The context options.</param>
        public ToDoAppDataBase(DbContextOptions options)
            : base(options)
        {

        }

        /// <summary>
        /// The Todo tasks db set.
        /// </summary>
        public DbSet<ToDoTask> ToDoTasks { get; set; }
    }
}
