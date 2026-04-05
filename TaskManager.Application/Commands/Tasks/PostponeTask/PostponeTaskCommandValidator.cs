using FluentValidation;

namespace TaskManager.Application.Commands.Tasks.PostponeTask;

public sealed class PostponeTaskCommandValidator : AbstractValidator<PostponeTaskCommand>
{
    public PostponeTaskCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Task ID is required.");
        RuleFor(x => x.NewDueDate)
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("New due date must be in the future.");
    }
}