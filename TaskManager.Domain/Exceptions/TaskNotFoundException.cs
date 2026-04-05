namespace TaskManager.Domain.Exceptions;

// Custom exception for when a task is not found. This exception is thrown when an operation is attempted on a task that does not exist in the repository.
public sealed class TaskNotFoundException : Exception
{
    public Guid TaskId { get; }

    // Constructor that initializes the exception with the task ID. The message is formatted to indicate that the task with the specified ID was not found.
    public TaskNotFoundException(Guid taskId)
        : base($"Task with ID '{taskId}' was not found.")
    {
        TaskId = taskId;
    }
}