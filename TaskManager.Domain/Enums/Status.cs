namespace TaskManager.Domain.Enums;

public enum Status
{
    InProgress = 1,
    WaitingForEvent = 2,
    Completed = 3,
    Cancelled = 4,
    Postponed = 5,
    New = 6
}