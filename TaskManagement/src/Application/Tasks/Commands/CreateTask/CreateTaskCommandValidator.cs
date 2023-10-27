using FluentValidation;

namespace Application.Tasks.Commands.CreateTask
{
    public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskCommandValidator()
        {
            RuleFor(t => t.Title)
                .NotEmpty();
            RuleFor(t => t.Description)
                .NotEmpty();
        }
    }
}
