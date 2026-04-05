using TaskManager.Domain.Interfaces;
using TaskManager.Domain.Exceptions;
using MediatR;

namespace TaskManager.Application.Commands.Tasks.PostponeTask;

public sealed class PostponeTaskCommandHandler : IRequestHandler<PostponeTaskCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublisher _publisher;

    public PostponeTaskCommandHandler(IUnitOfWork unitOfWork, IPublisher publisher)
    {
        _unitOfWork = unitOfWork;
        _publisher = publisher;
    }

    public async Task Handle(PostponeTaskCommand request, CancellationToken cancellationToken)
    {
        var taskItem = await _unitOfWork.Tasks.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new TaskNotFoundException(request.Id);

        taskItem.Postpone(request.NewDueDate);

        await _unitOfWork.Tasks.UpdateAsync(taskItem, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        foreach (var domainEvent in taskItem.DomainEvents)
        {
            await _publisher.Publish(domainEvent, cancellationToken);
        }

        taskItem.ClearDomainEvents();
    }
}