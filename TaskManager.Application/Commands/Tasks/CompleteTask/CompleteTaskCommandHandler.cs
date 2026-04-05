using TaskManager.Domain.Entities.Tasks;
using TaskManager.Domain.Interfaces;
using MediatR;
using TaskManager.Domain.Exceptions;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.Commands.Tasks.CompleteTask;

public sealed class CompleteTaskCommandHandler : IRequestHandler<CompleteTaskCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublisher _publisher;

    public CompleteTaskCommandHandler(IUnitOfWork unitOfWork, IPublisher publisher)
    {
        _unitOfWork = unitOfWork;
        _publisher = publisher;
    }

    public async Task Handle(CompleteTaskCommand request, CancellationToken cancellationToken)
    {
        var taskItem = await _unitOfWork.Tasks.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new TaskNotFoundException(request.Id);

        taskItem.MarkAsCompleted();

        await _unitOfWork.Tasks.UpdateAsync(taskItem, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        foreach (var domainEvent in taskItem.DomainEvents)
        {
            await _publisher.Publish(domainEvent, cancellationToken);
        }

        taskItem.ClearDomainEvents();
    }

}