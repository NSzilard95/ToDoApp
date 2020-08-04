using ToDoApp.BusinessEntity.Model.Base;

namespace ToDoApp.BusinessEntity.Model
{
    /// <summary>
    /// The ToDoTask entity.
    /// </summary>
    public class ToDoTask : BaseEntity
    {
        /// <summary>
        /// Gets or sets the todo text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets whether the entity is done.
        /// </summary>
        public bool IsDone { get; set; }
    }
}
