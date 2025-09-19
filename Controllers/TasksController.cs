using Azure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System;
using UserTasksProject.Data; // Make sure this matches the actual namespace of ApplicationDbContext
using UserTasksProject.Models;
using UserTasksProject.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace UserTasksProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ApplicationDBContext dbContext; //private field to use in the different methods of this class to access the database

        public TasksController(ApplicationDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //we will add endpoints for CRUD operations here

        //reading all users from tasks table
        [HttpGet]
        public IActionResult GetAllTasks()
        {
            var allTasks = dbContext.Tasks.Include(t => t.Assignee).ToList();
            return Ok(allTasks);
        }


        //Get single Task
        [HttpGet]
        [Route("{ID:guid}")]
        public IActionResult GetTaskByID(Guid ID)
        {
            var task = dbContext.Tasks.Include(t => t.Assignee).FirstOrDefault(t => t.ID == ID);

            if (task is null)
            {
                return NotFound();
            }

            return Ok(task);

        }

        //Add a new task
        [HttpPost]
        public IActionResult AddTask(AddTaskDto addTaskDto)
        {   
            //need to convert TaskDto to Tasks entity so we need the below variable to do that
            var taskEntity = new Tasks()
            {
                Title = addTaskDto.Title,
                Description = addTaskDto.Description,
                
                DueDate = addTaskDto.DueDate,
                AssigneeID = addTaskDto.AssigneeID,
               // Assignee = addTaskDto.AssigneeUsername
                //we did this because entities are separate from Dtos and therefore achieve a separation of concerns

            };


            dbContext.Tasks.Add(taskEntity);
            dbContext.SaveChanges(); //we need this because Entity Framework core wants us to save the changes ourselve, after this the changes are transferred to DB and the user is added
            var createdTask = dbContext.Tasks.Include(t => t.Assignee).FirstOrDefault(t => t.ID == taskEntity.ID);
            return Ok(createdTask);

            

        }

        //Update a task
        [HttpPut]
        [Route("{ID:guid}")]

        public IActionResult UpdateTask(Guid ID, UpdateTaskDto updateTaskDto)
        {
            var task = dbContext.Tasks.Find(ID);
            if (task is null)
            {
                return NotFound();
            }
            //update the properties of the existing user with the values from the updateUserDto
            task.Title = updateTaskDto.Title;
            task.Description = updateTaskDto.Description;
            task.AssigneeID = updateTaskDto.AssigneeID;
            task.DueDate = updateTaskDto.DueDate;
            //task.Assignee = updateTaskDto.AssigneeUsername;

            dbContext.SaveChanges(); //save the changes to the database
            var updatedTask = dbContext.Tasks.Include(t => t.Assignee).FirstOrDefault(t => t.ID == task.ID);
            return Ok(updatedTask);
        }





        //Delete a task
        [HttpDelete]
        [Route("{ID:guid}")]


        public IActionResult DeleteTask(Guid ID)
        {
            var task = dbContext.Tasks.Find(ID);

            if (task is null)
            {
                return NotFound();
            }
            dbContext.Tasks.Remove(task);
            dbContext.SaveChanges();

            return Ok();
        }


        [HttpGet("expired")]
        public IActionResult GetExpiredTasks()
        {
            var expiredTasks = dbContext.Tasks
                .Include(t => t.Assignee)
                .Where(t => t.DueDate < DateTime.Today)
                .ToList();

            return Ok(expiredTasks);
        }

        // Get all active tasks (due date today or in the future)
        [HttpGet("active")]
        public IActionResult GetActiveTasks()
        {
            var activeTasks = dbContext.Tasks
                .Include(t => t.Assignee)
                .Where(t => t.DueDate >= DateTime.Today)
                .ToList();

            return Ok(activeTasks);
        }

        // Get all tasks from a certain date
        [HttpGet("date/{date}")]
        public IActionResult GetTasksByDate(DateTime date)
        {
            var tasks = dbContext.Tasks
                .Include(t => t.Assignee)
                .Where(t => t.DueDate.Date == date.Date)
                .ToList();

            return Ok(tasks);
        }

        // Get all tasks assigned to a specific user
        [HttpGet("user/{userID}")]
        public IActionResult GetTasksByUser(Guid userID)
        {
            var tasks = dbContext.Tasks
                .Include(t => t.Assignee)
                .Where(t => t.AssigneeID == userID)
                .ToList();

            return Ok(tasks);
        }

    }
}

