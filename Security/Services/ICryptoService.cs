namespace Security.Services
{
    public interface ICryptoService
    {
        string GenerateSalt();

        string Hash(string plaintext, string salt);

        string GenerateRandomHash();

        bool CheckPassword(string hashedpassword, string plaintext, string salt);
    }
}
