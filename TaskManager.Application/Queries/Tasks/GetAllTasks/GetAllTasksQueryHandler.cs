using MediatR;
using TaskManager.Application.DTOs;
using TaskManager.Domain.Exceptions;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Application.Queries.Tasks.GetAllTasks;

// Handler for the GetAllTasksQuery. This class is responsible for handling the query to retrieve all tasks from the database and returning them as a collection of TaskItemDto objects.
public sealed class GetAllTasksQueryHandler : IRequestHandler<GetAllTasksQuery, IEnumerable<TaskItemDto>>
{
    // The IUnitOfWork instance is injected into the handler to allow access to the task repository. This enables the handler to retrieve all tasks from the database.
    private readonly IUnitOfWork _unitOfWork;

    // Constructor that initializes the handler with the IUnitOfWork instance. This allows the handler to access the task repository to perform database operations.
    public GetAllTasksQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // Handles the GetAllTasksQuery by retrieving all tasks from the database and mapping them to a collection of TaskItemDto objects. The method is asynchronous and returns a task that resolves to an IEnumerable of TaskItemDto.
    public async Task<IEnumerable<TaskItemDto>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
    {
        // Retrieve all tasks from the database using the task repository. The GetAllAsync method is called on the Tasks repository, which returns a collection of TaskItem entities.
        var taskItem = await _unitOfWork.Tasks.GetAllAsync(cancellationToken);

        // Map the collection of TaskItem entities to a collection of TaskItemDto objects. This is done using LINQ's Select method, which projects each TaskItem into a new TaskItemDto with the corresponding properties.
        return taskItem.Select(t => new TaskItemDto
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            Status = t.Status,
            DueDate = t.DueDate,
            CreatedAt = t.CreatedAt,
            UpdatedAt = t.UpdatedAt,
            CompletedAt = t.CompletedAt,
            CancelledAt = t.CancelledAt
        });
    }
}