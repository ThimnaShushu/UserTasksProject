
using Microsoft.EntityFrameworkCore;
using UserTasksProject.Models.Entities;

namespace UserTasksProject.Data
{
    public class ApplicationDBContext : DbContext
    {
        //constructor
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {
            
        }

        //Add a property for the collection(Users) we will store in the database
        public DbSet<User> Users { get; set; }
        public DbSet<Tasks> Tasks { get; set; }


    }
}
