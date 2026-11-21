namespace StudyPro.Models;

public class StudyTask
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Subject { get; set; }

    public string? Notes { get; set; }

    public DateTime? DueDate { get; set; }

    public bool IsCompleted { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}