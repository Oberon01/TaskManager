namespace TaskManager.Domain.Interfaces;

// encapsulates all of the interfaces for repositories and other data access related operations, ensuring that all changes are committed as a single unit of work.
public interface IUnitOfWork : IDisposable
{
    // Expose ITaskRepository to allow access to task-related data operations
    ITaskRepository Tasks { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}