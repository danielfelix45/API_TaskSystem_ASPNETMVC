using Microsoft.AspNetCore.Mvc;
using TaskSystem.Models;
using TaskSystem.Repositories.Interfaces;

namespace TaskSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;
        public TaskController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<TaskModel>>> GetAll()
        {
            List<TaskModel> tasks = await _taskRepository.GetAll();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<TaskModel>>> GetById(int id)
        {
            TaskModel task = await _taskRepository.GetById(id);
            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<List<TaskModel>>> Register([FromBody] TaskModel taskModel)
        {
            TaskModel task = await _taskRepository.ToAdd(taskModel);
            return Ok(task);
        }

        [HttpPut]
        public async Task<ActionResult<List<TaskModel>>> Update([FromBody] TaskModel taskModel, int id)
        {
            taskModel.Id = id;
            TaskModel task = await _taskRepository.ToUpdate(taskModel, id);
            return Ok(task);
        }

        [HttpDelete]
        public async Task<ActionResult<List<TaskModel>>> Delete(int id)
        {
            bool deleted = await _taskRepository.ToDelete(id);
            return Ok(deleted);
        }
    }
}
