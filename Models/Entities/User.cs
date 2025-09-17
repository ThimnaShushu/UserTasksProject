namespace UserTasksProject.Models.Entities
{
    public class User
    {
        //properties (the columns of the table in the SqlServer Database
        public Guid ID { get; set; } //Primary Key
        public required string Username { get; set; } //made these properties to be required because all instances of this class must have these properties
        public required string Email { get; set; }
        public required string Password { get; set; }
        
    }
}
