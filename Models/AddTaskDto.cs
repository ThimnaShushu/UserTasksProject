using UserTasksProject.Models.Entities;

namespace UserTasksProject.Models
{
    public class AddTaskDto
    {
        public required string Title { get; set; } //made these properties to be required because all instances of this class must have these properties
        public required string Description { get; set; }
        public required DateTime DueDate { get; set; }

        public required Guid AssigneeID { get; set; } // Navigation property to the User entity since each task belongs to one user
        public User? AssigneeUsername
        {
            get; set;
        }
    }
}
