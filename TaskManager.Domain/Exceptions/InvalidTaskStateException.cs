namespace TaskManager.Domain.Exceptions;

// Custom exception for invalid task state transitions. This exception is thrown when an action is attempted on a task that is not in a valid state for that action.
public sealed class InvalidTaskStateException : Exception
{
    public Guid TaskId { get; }
    public string AttemptedAction { get; }
    public string CurrentState { get; }

    // Constructor that initializes the exception with the task ID, current state, and attempted action. The message is formatted to provide clear information about the error.
    public InvalidTaskStateException(Guid taskId, string currentState, string attemptedAction)
        : base($"Cannot perform '{attemptedAction}' on task with ID '{taskId}' in state '{currentState}'.")
    {
        TaskId = taskId;
        AttemptedAction = attemptedAction.ToLowerInvariant();
        CurrentState = currentState.ToLowerInvariant();
    }

}