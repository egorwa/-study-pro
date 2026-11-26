using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudyPro.Data;
using StudyPro.Models;

namespace StudyPro.Features.Account;

public interface IAccountService
{
    Task<RegisterResult> RegisterAsync(RegisterInput input, CancellationToken ct = default);
    Task<(User? User, string? Error, PasswordVerificationResult Status)> VerifyAsync(
        string email, string password, CancellationToken ct = default);
}

public sealed class AccountService : IAccountService
{
    private readonly IDbContextFactory<AppDbContext> _factory;
    private readonly IPasswordHasher<User> _hasher;

    public AccountService(IDbContextFactory<AppDbContext> factory, IPasswordHasher<User> hasher)
    {
        _factory = factory;
        _hasher = hasher;
    }

    public async Task<RegisterResult> RegisterAsync(RegisterInput input, CancellationToken ct = default)
    {
        var email = input.Email.Trim().ToLowerInvariant();
        await using var db = await _factory.CreateDbContextAsync(ct);

        if (await db.Users.AnyAsync(u => u.Email == email, ct))
            return RegisterResult.FailField(nameof(RegisterInput.Email), "That email is already registered.");

        if (input.Dob >= DateTime.UtcNow.Date || input.Dob > DateTime.UtcNow.Date.AddYears(-13))
            return RegisterResult.FailField(nameof(RegisterInput.Dob), "You must be at least 13 years old.");

        var user = new User
        {
            Name      = input.Name.Trim(),
            LastName  = input.LastName.Trim(),
            Dob       = input.Dob,
            Email     = email,
            Phone     = string.IsNullOrWhiteSpace(input.Phone) ? null : input.Phone.Trim(),
            CreatedAt = DateTime.UtcNow
        };
        user.HashedPass = _hasher.HashPassword(user, input.Password);

        try
        {
            db.Users.Add(user);
            await db.SaveChangesAsync(ct);
        }
        catch (DbUpdateException)
        {
            return RegisterResult.FailGeneral("Something went wrong creating your account. Please try again.");
        }

        return RegisterResult.Ok(user);
    }

    public async Task<(User? User, string? Error, PasswordVerificationResult Status)> VerifyAsync(
        string email, string password, CancellationToken ct = default)
    {
        var normalized = email.Trim().ToLowerInvariant();
        await using var db = await _factory.CreateDbContextAsync(ct);
        var user = await db.Users.FirstOrDefaultAsync(u => u.Email == normalized, ct);

        if (user is null)
            return (null, "Invalid email or password.", PasswordVerificationResult.Failed);

        var result = _hasher.VerifyHashedPassword(user, user.HashedPass, password);
        if (result == PasswordVerificationResult.Failed)
            return (null, "Invalid email or password.", result);

        if (result == PasswordVerificationResult.SuccessRehashNeeded)
        {
            user.HashedPass = _hasher.HashPassword(user, password);
            await db.SaveChangesAsync(ct);
        }

        return (user, null, result);
    }
}