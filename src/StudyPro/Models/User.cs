using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StudyPro.Models;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required, StringLength(50)]
    public string Name { get; set; } = string.Empty;

    [Required, StringLength(50)]
    public string LastName { get; set; } = string.Empty;

    [Required, DataType(DataType.Date)]
    public DateTime Dob { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required, EmailAddress, StringLength(254)]
    public string Email { get; set; } = string.Empty;

    [Phone, StringLength(20)]
    public string? Phone { get; set; }

    [Required, JsonIgnore]
    public string HashedPass { get; set; } = string.Empty;

    public List<Guid> AssignmentIds { get; set; } = new();
}