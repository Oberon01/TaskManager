using TaskManager.Domain.Enums;
using TaskManager.Domain.Primitives;

namespace TaskManager.Domain.Entities.Tasks;

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

    public static TaskItem Create(string title, string description, DateTime dueDate)
    {
        ArgumentException.ThrowIfNullOrEmpty(title, nameof(title));
        ArgumentException.ThrowIfNullOrEmpty(description, nameof(description));

        var taskItem = new TaskItem
        {
            Id = Guid.NewGuid(),
            Title = title,
            Description = description,
            DueDate = dueDate,
            Status = Status.New,
            CreatedAt = DateTime.UtcNow
        };

        taskItem.RaiseDomainEvent(new Events.TaskCreatedEvent(
            taskItem.Id,
            taskItem.Title,
            taskItem.Status,
            taskItem.CreatedAt
        ));

        return taskItem;
    }

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

    public void Resume()
    {
        if (Status != Status.WaitingForEvent)
            throw new InvalidOperationException("Only tasks that are waiting for an event can be resumed.");

        Status = Status.InProgress;
        UpdatedAt = DateTime.UtcNow;

        RaiseDomainEvent(new Events.TaskResumedEvent(
            Id,
            Title,
            UpdatedAt.Value
        ));
    }
    
    public void Cancel()
    {
        if (Status == Status.Completed)
            throw new InvalidOperationException("Completed tasks cannot be cancelled.");
        Status = Status.Cancelled;
        UpdatedAt = DateTime.UtcNow;
        CancelledAt = DateTime.UtcNow;

        RaiseDomainEvent(new Events.TaskCancelledEvent(
            Id,
            Title,
            CancelledAt.Value
        ));
    }

    public void Postpone(DateTime newDueDate)
    {
        if (Status == Status.Completed)
            throw new InvalidOperationException("Completed tasks cannot be postponed.");
        DueDate = newDueDate;
        UpdatedAt = DateTime.UtcNow;
        RaiseDomainEvent(new Events.TaskPostponedEvent(
            Id,
            Title,
            DueDate,
            UpdatedAt.Value
        ));
    }

    public void MarkAsCompleted()
    {
        if (Status != Status.Completed)
            throw new InvalidOperationException("Only tasks that are not completed can be marked as completed.");

        if (Status == Status.Completed)
            throw new InvalidOperationException("Task is already completed.");

        Status = Status.Completed;
        CompletedAt = DateTime.UtcNow;

        RaiseDomainEvent(new Events.TaskCompletedEvent(
            Id,
            Title,
            CompletedAt.Value
        ));
    }


}