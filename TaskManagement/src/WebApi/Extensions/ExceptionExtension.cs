using Domain.Exceptions;

namespace WebApi.Extensions
{
    public static class ExceptionExtension
    {
        public static int GetStatusCode(this Exception exception)
        {
            var statusCode = exception switch
            {
                ValidationException => StatusCodes.Status400BadRequest,
                Application.Common.Exceptions.ValidationException => StatusCodes.Status400BadRequest,
                NotFoundException => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };

            return statusCode;
        }
    }
}
