﻿namespace ToDoApp.BusinessEntity.Model.Base
{
    /// <summary>
    /// The base entity
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets whether the entity is deleted.
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
