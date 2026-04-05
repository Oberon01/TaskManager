using MediatR;

namespace TaskManager.Application.Commands.Tasks.CreateTask;

public sealed record CreateTaskCommand(
    string Title,
    string Description,
    DateTime DueDate
) : IRequest<Guid>;