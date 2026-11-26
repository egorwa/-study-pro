using StudyPro.Models;

namespace StudyPro.Features.Account;

public sealed record RegisterResult(User? User, string? FieldName, string? Error, string? GeneralError)
{
    public bool Success => User is not null;

    public static RegisterResult Ok(User user) => new(user, null, null, null);
    public static RegisterResult FailField(string field, string message) => new(null, field, message, null);
    public static RegisterResult FailGeneral(string message) => new(null, null, null, message);
}