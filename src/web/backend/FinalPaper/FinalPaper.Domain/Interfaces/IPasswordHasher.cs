using Microsoft.AspNetCore.Identity;

namespace FinalPaper.Domain.Interfaces;

public interface IPasswordHasher
{
    public string HashPassword(string password);

    public PasswordVerificationResult VerifyHashedPassword(string hashedPassword,
        string providedPassword);
}