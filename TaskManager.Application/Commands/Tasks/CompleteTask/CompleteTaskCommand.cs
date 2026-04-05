using MediatR;

namespace TaskManager.Application.Commands.Tasks.CompleteTask;

public sealed record CompleteTaskCommand(
    Guid Id
) : IRequest;