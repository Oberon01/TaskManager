using MediatR;

namespace TaskManager.Application.Commands.Tasks.ResumeTask;

public sealed record ResumeTaskCommand(
    Guid Id
) : IRequest;