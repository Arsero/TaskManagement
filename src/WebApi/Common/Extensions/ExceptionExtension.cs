using Application.Common.Exceptions;
using WebApi.Common.Exceptions;

namespace WebApi.Common.Extensions
{
    public static class ExceptionExtension
    {
        public static int GetStatusCode(this Exception exception)
        {
            var statusCode = exception switch
            {
                InvalidDateException => StatusCodes.Status400BadRequest,
                ValidationException => StatusCodes.Status400BadRequest,
                BadRequestException => StatusCodes.Status400BadRequest,
                NotFoundException => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };

            return statusCode;
        }

        public static Dictionary<string, object?>? GetErrors(this Exception exception)
        {
            var validationException = exception as ValidationException;
            if (validationException != null && validationException.Errors.Count() > 1)
            {
                return validationException.GetErrorsDictionary();
            }

            return null;
        }
    }
}
