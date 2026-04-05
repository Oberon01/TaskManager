using TaskManager.Domain.Interfaces;
using TaskManager.Domain.Exceptions;
using MediatR;

namespace TaskManager.Application.Commands.Tasks.ResumeTask;

public sealed class ResumeTaskCommandHandler : IRequestHandler<ResumeTaskCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublisher _publisher;

    public ResumeTaskCommandHandler(IUnitOfWork unitOfWork, IPublisher publisher)
    {
        _unitOfWork = unitOfWork;
        _publisher = publisher;
    }

    public async Task Handle(ResumeTaskCommand request, CancellationToken cancellationToken)
    {
        var taskItem = await _unitOfWork.Tasks.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new TaskNotFoundException(request.Id);

        taskItem.Resume();

        await _unitOfWork.Tasks.UpdateAsync(taskItem, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        foreach (var domainEvent in taskItem.DomainEvents)
        {
            await _publisher.Publish(domainEvent, cancellationToken);
        }

        taskItem.ClearDomainEvents();
    }
}