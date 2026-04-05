namespace TaskManager.Domain.Primitives;

public abstract class Entity
{
    private readonly List<object> _domainEvents = new List<object>();

    public IReadOnlyCollection<object> DomainEvents => _domainEvents.AsReadOnly();

    protected void RaiseDomainEvent(object domainEvent) => _domainEvents.Add(domainEvent);

    public void ClearDomainEvents() => _domainEvents.Clear();
}