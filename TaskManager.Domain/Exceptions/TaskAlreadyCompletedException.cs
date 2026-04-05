// Exception class for handling cases where a task is already marked as completed.
namespace TaskManager.Domain.Exceptions;

public sealed class TaskAlreadyCompletedException : Exception
{
    public Guid TaskId { get; }

    public TaskAlreadyCompletedException(Guid taskId)
        : base($"Task with ID '{taskId}' is already marked as completed.")
    {
        TaskId = taskId;
    }
}