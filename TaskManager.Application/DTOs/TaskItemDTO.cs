using TaskManager.Domain.Enums;

namespace TaskManager.Application.DTOs;

public sealed class TaskItemDTO
{
    public Guid Id { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public DateTime DueDate { get; init; }
    public Status Status { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
    public DateTime? CompletedAt { get; init; }
    public DateTime? CancelledAt { get; init; }
}