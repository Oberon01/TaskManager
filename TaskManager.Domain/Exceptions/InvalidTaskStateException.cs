using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Exceptions;

// Custom exception for handling invalid state transitions of a task. This exception is thrown when an attempt is made to change a task's state to an invalid new state based on its current state.
public sealed class InvalidTaskStateException : Exception
{
    // Properties to hold the task ID, the new state that was attempted, and the current state of the task. These properties provide context for the exception and can be used for logging or debugging purposes.
    public Guid TaskId { get; }
    public Status NewState { get; }
    public Status CurrentState { get; }

    // Constructor that initializes the exception with the task ID, current state, and new state. The message is formatted to indicate the invalid state transition.
    public InvalidTaskStateException(Guid taskId, Status currentState, Status newState)
        : base($"Cannot transition from {currentState} to {newState}.")
    {
        TaskId = taskId;
        NewState = newState;
        CurrentState = currentState;
    }

}