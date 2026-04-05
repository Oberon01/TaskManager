using MediatR;

namespace TaskManager.Application.Commands.Tasks.PostponeTask;

public sealed record PostponeTaskCommand(
    Guid Id,
    DateTime NewDueDate
) : IRequest;