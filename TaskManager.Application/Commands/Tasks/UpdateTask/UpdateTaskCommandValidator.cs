using FluentValidation;

namespace TaskManager.Application.Commands.Tasks.UpdateTask;

public sealed class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
{
    public UpdateTaskCommandValidator()
    {
        // @TODO: Ensure updates reject invalid dates (e.g., past due dates).
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Task ID is required.");
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title is required.")
            .MaximumLength(100)
            .WithMessage("Title must not exceed 100 characters.");
        RuleFor(x => x.DueDate)
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("Due date must be in the future.");
    }
}