using System.Security.Cryptography;
using FinalPaper.Domain.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;

namespace FinalPaper.Infrastructure.Services;

/// <summary>
///     Taken from the ASP.NET Identity source code:
///     https://github.com/dotnet/aspnetcore/blob/main/src/Identity/Extensions.Core/src/PasswordHasher.cs
/// </summary>
public sealed class PasswordHasher : IPasswordHasher
{
    /* =======================
     * HASHED PASSWORD FORMATS
     * =======================
     *
     * Version 3:
     * PBKDF2 with HMAC-SHA256, 128-bit salt, 256-bit subKey, 10000 iterations.
     * Format: { 0x01, prf (UInt32), iter count (UInt32), salt length (UInt32), salt, subKey }
     * (All UInt32s are stored big-endian.)
     */

    private readonly int iterationCount = 10000;
    private readonly RandomNumberGenerator randomNumberGenerator;

    public PasswordHasher()
    {
        randomNumberGenerator = RandomNumberGenerator.Create();
    }

    public string HashPassword(string password)
    {
        if (string.IsNullOrEmpty(password))
            throw new ArgumentNullException(nameof(password));

        return Convert.ToBase64String(HashPasswordV3(password, randomNumberGenerator));
    }

    public PasswordVerificationResult VerifyHashedPassword(string hashedPassword,
        string providedPassword)
    {
        if (string.IsNullOrEmpty(hashedPassword))
            throw new ArgumentNullException(nameof(hashedPassword));

        if (string.IsNullOrEmpty(providedPassword))
            throw new ArgumentNullException(nameof(providedPassword));

        var decodedHashedPassword = Convert.FromBase64String(hashedPassword);
        if (decodedHashedPassword.Length == 0)
            return PasswordVerificationResult.Failed;


        switch (decodedHashedPassword[0])
        {
            case 0x00:
                int embeddedIterationCount;
                if (VerifyHashedPasswordV3(decodedHashedPassword, providedPassword, out embeddedIterationCount))
                    // If this hasher was configured with a higher iteration count, change the entry now.
                    return embeddedIterationCount < iterationCount
                        ? PasswordVerificationResult.SuccessRehashNeeded
                        : PasswordVerificationResult.Success;
                return PasswordVerificationResult.Failed;

            default:
                return PasswordVerificationResult.Failed; // unknown format marker
        }
    }

    private byte[] HashPasswordV3(string password, RandomNumberGenerator randomNumberGenerator)
    {
        return HashPasswordV3(password, randomNumberGenerator,
            KeyDerivationPrf.HMACSHA256,
            iterationCount,
            128 / 8,
            256 / 8);
    }

    private static byte[] HashPasswordV3(string password, RandomNumberGenerator rng, KeyDerivationPrf prf,
        int iterCount, int saltSize, int numBytesRequested)
    {
        var salt = new byte[saltSize];
        rng.GetBytes(salt);
        var subKey = KeyDerivation.Pbkdf2(password, salt, prf, iterCount, numBytesRequested);

        var outputBytes = new byte[13 + salt.Length + subKey.Length];
        outputBytes[0] = 0x00; // format marker
        WriteNetworkByteOrder(outputBytes, 1, (uint)prf);
        WriteNetworkByteOrder(outputBytes, 5, (uint)iterCount);
        WriteNetworkByteOrder(outputBytes, 9, (uint)saltSize);
        Buffer.BlockCopy(salt, 0, outputBytes, 13, salt.Length);
        Buffer.BlockCopy(subKey, 0, outputBytes, 13 + saltSize, subKey.Length);
        return outputBytes;
    }

    private static uint ReadNetworkByteOrder(byte[] buffer, int offset)
    {
        return ((uint)buffer[offset + 0] << 24)
               | ((uint)buffer[offset + 1] << 16)
               | ((uint)buffer[offset + 2] << 8)
               | buffer[offset + 3];
    }

    private static bool VerifyHashedPasswordV3(byte[] hashedPassword, string password, out int iterationCount)
    {
        iterationCount = default;

        try
        {
            // Read header information
            var prf = (KeyDerivationPrf)ReadNetworkByteOrder(hashedPassword, 1);
            iterationCount = (int)ReadNetworkByteOrder(hashedPassword, 5);
            var saltLength = (int)ReadNetworkByteOrder(hashedPassword, 9);

            // Read the salt: must be >= 128 bits
            if (saltLength < 128 / 8) return false;

            var salt = new byte[saltLength];
            Buffer.BlockCopy(hashedPassword, 13, salt, 0, salt.Length);

            // Read the subkey (the rest of the payload): must be >= 128 bits
            var subKeyLength = hashedPassword.Length - 13 - salt.Length;
            if (subKeyLength < 128 / 8) return false;

            var expectedSubKey = new byte[subKeyLength];
            Buffer.BlockCopy(hashedPassword, 13 + salt.Length, expectedSubKey, 0, expectedSubKey.Length);

            // Hash the incoming password and verify it
            var actualSubKey = KeyDerivation.Pbkdf2(password, salt, prf, iterationCount, subKeyLength);
#if NETSTANDARD2_0 || NETFRAMEWORK
            return ByteArraysEqual(actualSubkey, expectedSubkey);
#elif NETCOREAPP
            return CryptographicOperations.FixedTimeEquals(actualSubKey, expectedSubKey);
#else
#error Update target frameworks
#endif
        }
        catch
        {
            // This should never occur except in the case of a malformed payload, where
            // we might go off the end of the array. Regardless, a malformed payload
            // implies verification failed.
            return false;
        }
    }

    private static void WriteNetworkByteOrder(byte[] buffer, int offset, uint value)
    {
        buffer[offset + 0] = (byte)(value >> 24);
        buffer[offset + 1] = (byte)(value >> 16);
        buffer[offset + 2] = (byte)(value >> 8);
        buffer[offset + 3] = (byte)(value >> 0);
    }
}