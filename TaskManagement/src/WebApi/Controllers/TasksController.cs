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
        public async Task<ActionResult<IEnumerable<Domain.Task>>> GetTask()
        {
            return Ok(await _taskService.GetAllTasks());
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

        // POST: api/tasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Domain.Task>> PostTask(Domain.Task task)
        {
            task.Id = 0;
            await _taskService.AddTask(task);

            return CreatedAtAction("GetTask", new { id = task.Id }, task);
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

            try
            {
                await _taskService.UpdateTaskById(id, task);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return NoContent();
        }

        // PUT: api/tasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchTask(int id)
        {
            try
            {
                await _taskService.CompleteTask(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/tasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            await _taskService.RemoveTaskById(id);
            return NoContent();
        }
    }
}
