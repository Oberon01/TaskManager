using TaskManager.Domain.Entities.Tasks;
using TaskManager.Domain.Interfaces;
using MediatR;
using TaskManager.Domain.Exceptions;

namespace TaskManager.Application.Commands.Tasks.CancelTask;

// Command handler for canceling a task. It retrieves the task by ID, cancels it, updates the repository, and publishes any domain events.
public sealed class CancelTaskCommandHandler : IRequestHandler<CancelTaskCommand>
{
    // Dependencies: IUnitOfWork for data access and IPublisher for publishing domain events.
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublisher _publisher;

    // Constructor to inject dependencies.
    public CancelTaskCommandHandler(IUnitOfWork unitOfWork, IPublisher publisher)
    {
        // Initialize dependencies.
        _unitOfWork = unitOfWork;
        _publisher = publisher;
    }

    // Handle method to process the CancelTaskCommand.
    public async Task Handle(CancelTaskCommand request, CancellationToken cancellationToken)
    {
        // Retrieve the task by ID. If not found, throw a KeyNotFoundException.
        var taskItem = await _unitOfWork.Tasks.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new TaskNotFoundException(request.Id);

        // Cancel the task using the domain method.
        taskItem.Cancel();

        // Update the task in the repository and save changes.
        await _unitOfWork.Tasks.UpdateAsync(taskItem, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Publish any domain events that were generated during the cancellation process.
        foreach (var domainEvent in taskItem.DomainEvents)
        {
            await _publisher.Publish(domainEvent, cancellationToken);
        }

        // Clear domain events after publishing to prevent duplicate handling.
        taskItem.ClearDomainEvents();

    }
}