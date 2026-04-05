using TaskManager.Domain.Primitives;

namespace TaskManager.Domain.Events;

public sealed record TaskPostponedEvent(
    Guid Id,
    string Title,
    DateTime PostponedAt,
    DateTime UpdatedAt
) : IDomainEvent
{
    public Guid EventId { get; } = Guid.NewGuid();
    public DateTime OccurredOn {  get; } = DateTime.UtcNow;
}