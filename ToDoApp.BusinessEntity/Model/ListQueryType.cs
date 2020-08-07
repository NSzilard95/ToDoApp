namespace ToDoApp.BusinessEntity.Model
{
    /// <summary>
    /// Enum for determine the list query type.
    /// </summary>
    public enum ListQueryType
    {
        /// <summary>
        /// Query all todos.
        /// </summary>
        All = 0,

        /// <summary>
        /// Query active todos.
        /// </summary>
        InProgress = 1,

        /// <summary>
        /// Query done todos.
        /// </summary>
        Done = 2
    }
}
