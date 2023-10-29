using FluentValidation.Results;

namespace Application.Common.Exceptions
{
    public class ValidationException : FluentValidation.ValidationException
    {
        public ValidationException(IEnumerable<ValidationFailure> errors) : base(errors)
        {

        }
    }
}
