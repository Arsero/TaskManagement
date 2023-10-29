using Application.Tasks.Commands.CompleteTask;
using Application.Tasks.Commands.CreateTask;
using Application.Tasks.Commands.DeleteTask;
using Application.Tasks.Commands.UpdateTask;
using Application.Tasks.Queries.GetTaskById;
using Application.Tasks.Queries.GetTasksWithPaginationAndFilter;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TasksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Domain.Entities.Task>>> GetTask([FromQuery] GetTasksWithPaginationFilterAndSortingQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        // GET: api/tasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Domain.Entities.Task>> GetTask(int id)
        {
            return Ok(await _mediator.Send(new GetTaskByIdQuery(id)));
        }

        // POST: api/tasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Domain.Entities.Task>> PostTask(CreateTaskCommand command)
        {
            var task = await _mediator.Send(command);

            return CreatedAtAction("GetTask", new { id = task.Id }, task);
        }

        // PUT: api/tasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask(int id, UpdateTaskCommand updateTaskCommand)
        {
            if (id != updateTaskCommand.Id)
            {
                throw new ValidationException("Not the same IDs.");
            }

            await _mediator.Send(updateTaskCommand);
            return NoContent();
        }

        // PUT: api/tasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchTask(int id)
        {
            await _mediator.Send(new CompleteTaskCommand(id));
            return NoContent();
        }

        // DELETE: api/tasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            await _mediator.Send(new DeleteTaskCommand(id));
            return NoContent();
        }
    }
}
