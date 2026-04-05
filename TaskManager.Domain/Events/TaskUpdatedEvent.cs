using TaskManager.Domain.Enums;
using TaskManager.Domain.Primitives;

namespace TaskManager.Domain.Events;

public sealed record TaskUpdatedEvent(
    Guid Id,
    string Title,
    Status Status,
    DateTime UpdatedAt
) : IDomainEvent
{
    public Guid EventId { get; } = Guid.NewGuid();
    public DateTime OccurredOn {  get; } = DateTime.UtcNow;
}