// Exception class for handling cases where a task is already marked as completed.
namespace TaskManager.Domain.Exceptions;

// This exception is thrown when an attempt is made to mark a task as completed, but the task is already marked as completed. It includes the ID of the task that caused the exception for better error handling and debugging.
public sealed class TaskAlreadyCompletedException : Exception
{
    // The ID of the task that is already marked as completed. This property allows the exception to carry specific information about the task that caused the error, which can be useful for logging and debugging purposes.
    public Guid TaskId { get; }

    // Constructor that initializes the exception with the task ID. The message is formatted to indicate that the task with the specified ID is already marked as completed.
    public TaskAlreadyCompletedException(Guid taskId)
        : base($"Task with ID '{taskId}' is already marked as completed.")
    {
        TaskId = taskId;
    }
}