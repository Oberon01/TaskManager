using TaskManager.Domain.Exceptions;
using TaskManager.Domain.Interfaces;
using MediatR;

namespace TaskManager.Application.Commands.Tasks.UpdateTask;

public sealed class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublisher _publisher;
    public UpdateTaskCommandHandler(IUnitOfWork unitOfWork, IPublisher publisher)
    {
        _unitOfWork = unitOfWork;
        _publisher = publisher;
    }

    // @TODO: Consider adding validation for the input data (e.g., title length, due date in the future) before updating the task.
    public async Task Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var taskItem = await _unitOfWork.Tasks.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new TaskNotFoundException(request.Id);

        taskItem.Update(request.Title, request.Description, request.DueDate);

        await _unitOfWork.Tasks.UpdateAsync(taskItem, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        foreach (var domainEvent in taskItem.DomainEvents)
        {
            await _publisher.Publish(domainEvent, cancellationToken);
        }

        taskItem.ClearDomainEvents();
    }
}