using MediatR;

namespace TaskManager.Application.Commands.Tasks.CancelTask;

public sealed record CancelTaskCommand(
    Guid Id
) : IRequest;