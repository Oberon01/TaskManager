using FluentValidation;

namespace TaskManager.Application.Commands.Tasks.PostponeTask;

public sealed class PostponeTaskCommandValidator : AbstractValidator<PostponeTaskCommand>
{
    public PostponeTaskCommandValidator()
    {
        // @TODO: Ensure postponing only allowed for tasks in certain states (e.g., InProgress, WaitingForEvent).
        // @TODO: Consider validating that the new due date is later than the current due date of the task.
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Task ID is required.");
        RuleFor(x => x.NewDueDate)
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("New due date must be in the future.");
    }
}