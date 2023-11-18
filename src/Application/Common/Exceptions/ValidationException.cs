using FluentValidation.Results;
using System.Collections.Immutable;

namespace Application.Common.Exceptions
{
    public class ValidationException : FluentValidation.ValidationException
    {
        public ValidationException(string message) : base(message)
        {
        }

        public ValidationException(IEnumerable<ValidationFailure> errors) : base("Validation failed", errors)
        {
        }

        public Dictionary<string, object?>? GetErrorsDictionary()
        {
            var dictionary = new Dictionary<string, object?>
            {
                {
                    "errors",
                    Errors.Select(e => new {e.PropertyName, e.ErrorMessage})
                        .ToDictionary(
                            e => System.Text.Json.JsonNamingPolicy.CamelCase.ConvertName(e.PropertyName), 
                            e => e.ErrorMessage)
                }
            };

            return dictionary;
        }
    }
}
