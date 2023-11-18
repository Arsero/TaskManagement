using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common.Extensions;

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
        public IResult Error()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
            _logger.LogError(exception, "Error {@Code} : {@Exception}",
                exception?.GetStatusCode(), 
                exception?.Message);

            return Results.Problem(title: exception?.Message,
                statusCode: exception?.GetStatusCode(),
                extensions: exception?.GetErrors());
        }
    }
}
