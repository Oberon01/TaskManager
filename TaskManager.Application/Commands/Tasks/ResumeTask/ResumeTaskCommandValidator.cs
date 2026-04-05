using FluentValidation;

namespace TaskManager.Application.Commands.Tasks.ResumeTask;

public sealed class ResumeTaskCommandValidator : AbstractValidator<ResumeTaskCommand>
{
    public ResumeTaskCommandValidator()
    {
        // @TODO: Ensure resuming only allowed for tasks in certain states (e.g., Paused).
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Task ID is required.");
    }
}