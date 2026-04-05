using TaskManager.Application.DTOs;
using MediatR;

namespace TaskManager.Application.Queries.Tasks.GetAllTasks;

// Query to get all tasks
public sealed record GetAllTasksQuery : IRequest<IEnumerable<TaskItemDto>>;