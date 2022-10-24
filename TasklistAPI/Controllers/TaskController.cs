using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TasklistAPI.DAL.Interfaces;
using TasklistAPI.Models;

namespace TasklistAPI.Controllers
{
    [EnableCors("PolicyOne")]
    [ApiController]
    [Route("api/[controller]")]

    public class TaskController : Controller
    {

        
        private readonly ITaskRepository _taskRepository;

        public TaskController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        // GET: TaskController
        [HttpGet]
        public IActionResult Index([FromQuery] PaginationParams @params)
        {
            PaginationResult data = _taskRepository.GetAll(@params);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(data.PaginationMetadata));
            return Ok(data.TaskList);
        }

        // GET: TaskController/Details/5
        [HttpGet("{id}")]
        public ActionResult Details(int id)
        {
            return Ok(_taskRepository.Get(id));
        }

        // GET: TaskController/Create
        [HttpPost]
        public ActionResult Create(TaskModel task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _taskRepository.Create(task);
            return Ok(task);
        }

        [HttpPost("Duplicate/{id}")]
        public ActionResult Duplicate(int id)
        {
            _taskRepository.Duplicate(id);
            return Ok();
        }



        // GET: TaskController/Edit/5
        [HttpPut]
        public ActionResult Edit([FromBody]TaskModel task)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _taskRepository.Update(task);
            return Ok(task);
        }



        // POST: TaskController/Delete/5
        [HttpDelete]
        public ActionResult Delete(List<int> idList)
        {
           _taskRepository.Delete(idList);
            return Ok();
        }



    }
}
