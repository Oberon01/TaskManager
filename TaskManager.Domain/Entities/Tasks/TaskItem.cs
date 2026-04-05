using TaskManager.Domain.Enums;
using TaskManager.Domain.Exceptions;
using TaskManager.Domain.Primitives;

namespace TaskManager.Domain.Entities.Tasks;

// Task entity representing a task item in the task management system. This class encapsulates the properties and behaviors of a task, including creation, updating, resuming, cancelling, postponing, and marking as completed. It also raises domain events for each significant action performed on the task.
public class TaskItem : Entity
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public DateTime DueDate { get; private set; }
    public Status Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    public DateTime? CancelledAt { get; private set; }

    private TaskItem() { }

    private static readonly Dictionary<Status, List<Status>> AllowedTransitions = new()
    {
        // @TODO: Consider revising state transition for states like Postponed.
        { Status.New, new List<Status> { Status.InProgress, Status.Cancelled } },
        { Status.InProgress, new List<Status> { Status.Completed, Status.WaitingForEvent, Status.Cancelled } },
        { Status.WaitingForEvent, new List<Status> { Status.InProgress, Status.Cancelled } },
        { Status.Postponed, new List<Status> { Status.InProgress, Status.Cancelled} }
    };

    private void ChangeStatus(Status newStatus)
    {
        if (!AllowedTransitions.ContainsKey(Status) || !AllowedTransitions[Status].Contains(newStatus))
            throw new InvalidTaskStateException(Id, Status, newStatus);

        Status = newStatus;
        UpdatedAt = DateTime.UtcNow;
    }

    public static TaskItem Create(string title, string description, DateTime dueDate)
    {
        // Validate input parameters to ensure that title and description are not null or empty.
        ArgumentException.ThrowIfNullOrEmpty(title, nameof(title));
        ArgumentException.ThrowIfNullOrEmpty(description, nameof(description));

        // Create a new instance of TaskItem with the provided title, description, and due date. The status is set to New, and the created timestamp is set to the current UTC time.
        var taskItem = new TaskItem
        {
            Id = Guid.NewGuid(),
            Title = title,
            Description = description,
            DueDate = dueDate,
            Status = Status.New,
            CreatedAt = DateTime.UtcNow
        };

        // Raise a domain event to indicate that a new task has been created. This event can be handled by other parts of the system to perform additional actions, such as sending notifications or updating related data.
        taskItem.RaiseDomainEvent(new Events.TaskCreatedEvent(
            taskItem.Id,
            taskItem.Title,
            taskItem.Status,
            taskItem.CreatedAt
        ));

        return taskItem;
    }

    // Update the task's title, description, and due date. This method also updates the UpdatedAt timestamp and raises a domain event to indicate that the task has been updated.
    // @TODO: Consider validating state state transitions for updates, e.g., preventing updates to completed or cancelled tasks.
    public void Update(string title, string description, DateTime dueDate)
    {
        ArgumentException.ThrowIfNullOrEmpty(title, nameof(title));
        ArgumentException.ThrowIfNullOrEmpty(description, nameof(description));

        Title = title;
        Description = description;
        DueDate = dueDate;
        UpdatedAt = DateTime.UtcNow;

        RaiseDomainEvent(new Events.TaskUpdatedEvent(
            Id,
            Title,
            Status,
            UpdatedAt.Value
        ));
    }

    // Resume a task that is currently waiting for an event.
    public void Resume()
    {
        ChangeStatus(Status.InProgress);

        // Raise domain event to indicate that the task has been resumed.
        RaiseDomainEvent(new Events.TaskResumedEvent(
            Id,
            Title,
            UpdatedAt.Value
        ));
    }
    
    // Cancel a task that's not yet been completed.
    public void Cancel()
    {
        ChangeStatus(Status.Cancelled);
        CancelledAt = DateTime.UtcNow;

        // Raise domain event to indicate that the task has been cancelled.
        RaiseDomainEvent(new Events.TaskCancelledEvent(
            Id,
            Title,
            CancelledAt.Value
        ));
    }

    // Postpone a task by updating its due date.
    public void Postpone(DateTime newDueDate)
    {
        // Ensuring 
        ChangeStatus(Status.Postponed);

        // Update the task's due date and set the UpdatedAt timestamp to the current UTC time.
        DueDate = newDueDate;

        // Raise domain event to indicate that the task has been postponed.
        RaiseDomainEvent(new Events.TaskPostponedEvent(
            Id,
            Title,
            DueDate,
            UpdatedAt.Value
        ));
    }

    // Mark a task as completed, updating its status and setting the CompletedAt timestamp. This method also raises a domain event to indicate that the task has been completed.
    public void MarkAsCompleted()
    {
        // Validation logic using private method to ensure state transition is valid.
        ChangeStatus(Status.Completed);
        CompletedAt = DateTime.UtcNow;

        // Raise domain event to indicate that the task has been marked as completed.
        RaiseDomainEvent(new Events.TaskCompletedEvent(Id, Title, CompletedAt.Value));
    }


}