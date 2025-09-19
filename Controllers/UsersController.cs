using Azure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserTasksProject.Data; // Make sure this matches the actual namespace of ApplicationDbContext
using UserTasksProject.Models;
using UserTasksProject.Models.Entities;
using Microsoft.EntityFrameworkCore;


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
            var allUsers = dbContext.Users.Include(u => u.AssignedTasks).ToList();
            return Ok(allUsers);
        }

        //Get single user
        [HttpGet]
        [Route("{ID:guid}")]
        public IActionResult GetUserByID(Guid ID)
        {
            var user = dbContext.Users.Include(u => u.AssignedTasks).FirstOrDefault(u => u.ID == ID);

            if (user is null)
            {
                return NotFound();
            }

            return Ok(user);

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

        //Update a user
        [HttpPut]
        [Route("{ID:guid}")]

        public IActionResult UpdateUser(Guid ID, UpdateUserDto updateUserDto)
        {
            var user = dbContext.Users.Find(ID);
            if (user is null)
            {
                return NotFound();
            }
            //update the properties of the existing user with the values from the updateUserDto
            user.Username = updateUserDto.Username;
            user.Email = updateUserDto.Email;
            user.Password = updateUserDto.Password;
            dbContext.SaveChanges(); //save the changes to the database
            return Ok(user);
        }





        //Delete a user
        [HttpDelete]
        [Route("{ID:guid}")]


        public IActionResult DeleteUser(Guid ID)
        {
            var user = dbContext.Users.Find(ID);

            if(user is null)
            {
                return NotFound();
            }
            dbContext.Users.Remove(user);
            dbContext.SaveChanges();

            return Ok();
        }



    }
}
