namespace UserTasksProject.Models.Entities
{
    public class Tasks
    {
        //properties (the columns of the table in the SqlServer Database
        public Guid ID { get; set; } //Primary Key
        public required string Title { get; set; } //made these properties to be required because all instances of this class must have these properties
        public required string Description { get; set; }
        public required DateTime DueDate { get; set; }
        public Guid AssigneeID { get; set; }
        public virtual User? Assignee { get; set; } // Navigation property to the User entity since each task belongs to one user
        
    }
}
