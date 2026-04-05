using FluentValidation;

namespace TaskManager.Application.Commands.Tasks.CompleteTask;

public sealed class CompleteTaskCommandValidator : AbstractValidator<CompleteTaskCommand>
{
    public CompleteTaskCommandValidator()
    {
        // @TODO: Block completion of tasks that are cancelled or already completed.
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Task ID is required.");
    }
}