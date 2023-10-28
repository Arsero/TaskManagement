using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;

namespace WebApi.Controllers
{
    [Route("api/error")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        private readonly ILogger<ErrorsController> _logger;

        public ErrorsController(ILogger<ErrorsController> logger)
        {
            this._logger = logger;
        }

        [AcceptVerbs("GET", "POST", "PUT", "DELETE", "PATCH")]
        public IActionResult Error()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
            _logger.LogError("An error occured : {@exception}", exception?.Message);
            return Problem(title: exception?.Message, statusCode: exception?.GetStatusCode());
        }
    }
}
