using TaskManager.Domain.Primitives;

namespace TaskManager.Domain.Events;

public sealed record TaskCancelledEvent(
    Guid Id,
    string Title,
    DateTime CancelledAt
) : IDomainEvent
{
    public Guid EventId { get; } = Guid.NewGuid();
    public DateTime OccurredOn {  get; } = DateTime.UtcNow;
}