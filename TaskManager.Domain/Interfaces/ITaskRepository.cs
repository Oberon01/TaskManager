
using TaskManager.Domain.Entities.Tasks;
using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Interfaces;

// Repository interface for managing TaskItem entities
public interface ITaskRepository
{
    // Asynchronous methods for CRUD operations on TaskItem entities
    Task AddAsync(TaskItem task, CancellationToken cancellationToken = default);
    Task UpdateAsync(TaskItem task, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid taskId, CancellationToken cancellationToken = default);
    Task<TaskItem?> GetByIdAsync(Guid taskId, CancellationToken cancellationToken = default);
    Task<IEnumerable<TaskItem>> GetAllAsync(CancellationToken cancellationToken = default);

    // Additional methods for querying TaskItem entities based on specific criteria
    Task<IEnumerable<TaskItem>> GetByStatusAsync(Status status, CancellationToken cancellationToken = default);
    Task<IEnumerable<TaskItem>> GetByDueDateAsync(DateTime dueDate, CancellationToken cancellationToken = default);
    Task<IEnumerable<TaskItem>> GetOverDueAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<TaskItem>> GetActiveTasksAsync(List<TaskItem> tasks, CancellationToken cancellationToken = default);
}