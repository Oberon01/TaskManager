using TaskManager.Domain.Primitives;

namespace TaskManager.Domain.Events;

public sealed record TaskCompletedEvent(
    Guid Id,
    string Title,
    DateTime CompletedAt
) : IDomainEvent
{
    public Guid EventId { get; } = Guid.NewGuid();
    public DateTime OccurredOn {  get; } = DateTime.UtcNow;
}