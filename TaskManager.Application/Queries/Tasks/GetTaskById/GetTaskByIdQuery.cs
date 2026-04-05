using TaskManager.Application.DTOs;
using MediatR;

namespace TaskManager.Application.Queries.Tasks.GetTaskById;

// Query to get a task by its ID
public sealed record GetTaskByIdQuery(Guid TaskId) : IRequest<TaskItemDto>;