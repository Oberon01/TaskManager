using TaskManager.Domain.Exceptions;
using TaskManager.Domain.Interfaces;
using TaskManager.Application.DTOs;
using MediatR;

namespace TaskManager.Application.Queries.Tasks.GetTaskById;

public sealed class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, TaskItemDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetTaskByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TaskItemDto> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
    {
        var taskItem = await _unitOfWork.Tasks.GetByIdAsync(request.TaskId, cancellationToken)
            ?? throw new TaskNotFoundException(request.TaskId);

        return new TaskItemDto
        {
            Id = taskItem.Id,
            Title = taskItem.Title,
            Description = taskItem.Description,
            DueDate = taskItem.DueDate,
            Status = taskItem.Status,
            CreatedAt = taskItem.CreatedAt,
            UpdatedAt = taskItem.UpdatedAt,
            CompletedAt = taskItem.CompletedAt,
            CancelledAt = taskItem.CancelledAt
        };
    }
}