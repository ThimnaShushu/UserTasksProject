namespace UserTasksProject.Models
{   
    public class AddUserDto
    {
        public required string Username { get; set; } //made these properties to be required because all instances of this class must have these properties
        public required string Email { get; set; }
        public required string Password { get; set; }

    }
}
