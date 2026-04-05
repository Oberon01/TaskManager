using MediatR;

namespace TaskManager.Application.Commands.Tasks.UpdateTask;

public sealed record UpdateTaskCommand(
    Guid Id,
    string Title,
    string Description,
    DateTime DueDate
) : IRequest;