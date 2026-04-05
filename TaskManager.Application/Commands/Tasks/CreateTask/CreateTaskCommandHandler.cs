using TaskManager.Domain.Interfaces;
using MediatR;
using TaskManager.Domain.Entities.Tasks;

namespace TaskManager.Application.Commands.Tasks.CreateTask;

public sealed class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublisher _publisher;

    public CreateTaskCommandHandler(IUnitOfWork unitOfWork, IPublisher publisher)
    {
        _unitOfWork = unitOfWork;
        _publisher = publisher;
    }

    public async Task<Guid> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var taskItem = TaskItem.Create(request.Title, request.Description, request.DueDate);

        await _unitOfWork.Tasks.AddAsync(taskItem, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        foreach (var domainEvent in taskItem.DomainEvents)
        {
            await _publisher.Publish(domainEvent, cancellationToken);
        }

        taskItem.ClearDomainEvents();

        return taskItem.Id;
    }
}