using TaskManager.Domain.Enums;
using TaskManager.Domain.Primitives;

namespace TaskManager.Domain.Events;

public sealed record TaskCreatedEvent(
    Guid Id,
    string Title,
    Status Status,
    DateTime CreatedAt
) : IDomainEvent
{
    public Guid EventId { get; } = Guid.NewGuid();
    public DateTime OccurredOn {  get; } = DateTime.UtcNow;
}