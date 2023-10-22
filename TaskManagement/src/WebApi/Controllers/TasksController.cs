using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Application;

namespace WebApi.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        // GET: api/tasks
        [HttpGet]
        public ActionResult<IEnumerable<Domain.Task>> GetTask()
        {
            return Ok(_taskService.GetAllTasks());
        }

        // GET: api/tasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Domain.Task>> GetTask(int id)
        {
            var task = await _taskService.GetTaskById(id);
            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        // PUT: api/tasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask(int id, Domain.Task task)
        {
            if (id != task.Id)
            {
                return BadRequest();
            }

            if (!(await _taskService.IsTaskExists(id)))
            {
                return NotFound();

            }

            _taskService.UpdateTaskById(task);
            return NoContent();
        }

        // PUT: api/tasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchTask(int id)
        {
            if (!(await _taskService.IsTaskExists(id)))
            {
                return NotFound();

            }

            _taskService.CompleteTask(id);
            return NoContent();
        }

        // POST: api/tasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Domain.Task> PostTask(Domain.Task task)
        {
            _taskService.AddTask(task);

            return CreatedAtAction("GetTask", new { id = task.Id }, task);
        }

        // DELETE: api/tasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            if (await _taskService.IsTaskExists(id))
            {
                _taskService.RemoveTaskById(id);
                return NoContent();
                
            }
            return NotFound();
        }
    }
}
