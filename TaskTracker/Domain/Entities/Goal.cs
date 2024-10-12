using TaskTracker.Domain.Enums;

namespace TaskTracker.Domain.Entities;

public class Goal
{
    public int Id { get; set; }
    public string? Description { get; set; }
    public Status Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}