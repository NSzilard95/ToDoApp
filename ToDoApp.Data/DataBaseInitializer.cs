using System.Collections.Generic;
using System.Linq;
using ToDoApp.BusinessEntity.Model;

namespace ToDoApp.Data
{
    /// <summary>
    /// The database initializer helper.
    /// </summary>
    public static class DataBaseInitializer
    {
        /// <summary>
        /// Database initializer.
        /// </summary>
        /// <param name="context"></param>
        public static void InitDefaultDatas(this ToDoAppDataBase context)
        {
            if (context.ToDoTasks.Count() < 1)
            {
                var defaultTasks = new List<ToDoTask>();

                for (int i = 1; i <= 30; i++)
                {
                    defaultTasks.Add(new ToDoTask
                    {
                         Text = "Todo task " + i,
                         IsDone = i % 2 == 0,
                         IsDeleted = i % 10 == 0
                    });
                }

                defaultTasks.ForEach(t => context.ToDoTasks.Add(t));

                context.SaveChanges();
            }
        }
    }
}
