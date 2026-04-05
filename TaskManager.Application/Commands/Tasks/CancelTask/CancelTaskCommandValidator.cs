using FluentValidation;

namespace TaskManager.Application.Commands.Tasks.CancelTask;

public sealed class CancelTaskCommandValidator : AbstractValidator<CancelTaskCommand>
{
    public CancelTaskCommandValidator()
    {
        // @TODO: Block cancellation of tasks that are already completed or cancelled.
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Task ID is required.");
    }
}
