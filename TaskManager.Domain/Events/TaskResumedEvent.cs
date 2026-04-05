using TaskManager.Domain.Primitives;

namespace TaskManager.Domain.Events;

public sealed record TaskResumedEvent(
    Guid Id,
    string Title,
    DateTime ResumedAt
) : IDomainEvent
{
    public Guid EventId { get; } = Guid.NewGuid();
    public DateTime OccurredOn {  get; } = DateTime.UtcNow;
}