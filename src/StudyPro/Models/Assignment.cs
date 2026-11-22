using System.ComponentModel.DataAnnotations;

namespace StudyPro.Models;

public enum AssignmentStatus
{
    Pending,
    InProgress,
    Completed
}

public class Assignment
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Course { get; set; } = string.Empty;

    [StringLength(1000)]
    public string? Notes { get; set; }

    public DateTime DueDate { get; set; }

    public AssignmentStatus Status { get; set; } = AssignmentStatus.Pending;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public string? UserId { get; set; }
}
