using Azure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserTasksProject.Data; // Make sure this matches the actual namespace of ApplicationDbContext
using UserTasksProject.Models;
using UserTasksProject.Models.Entities;


namespace UserTasksProject.Controllers
{   //localhost port number: /api/Users(name of the controller without the "Controller" suffix
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDBContext dbContext; //private field to use in the different methods of this class to access the database

        //need to connect to DB using DBContext so we will use constructor injection to get the instance of DBContext
        public UsersController(ApplicationDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //we will add endpoints for CRUD operations here

        //reading all users from users table
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var allUsers = dbContext.Users.ToList();
            return Ok(allUsers);
        }

        //Add a new user
        [HttpPost]
        public IActionResult AddUser(AddUserDto addUserDto)
        {
            //need to convert UserDto to User entity so we need the below variable to do that
            var userEntity = new User()
            {
                Username = addUserDto.Username,
                Email = addUserDto.Email,
                Password = addUserDto.Password
                //we did this because entities are separate from Dtos and therefore achieve a separation of concerns

            };


            dbContext.Users.Add(userEntity);
            dbContext.SaveChanges(); //we need this because Entity Framework core wants us to save the changes ourselve, after this the changes are transferred to DB and the user is added

            return Ok(userEntity);

        }
    }
}
