namespace TechSolutions.Application.Helper
{
    public static class PasswordHelper
    {
        public static string HashPassword(this string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("A senha não pode estar vazia ou nula.", nameof(password));
            }

            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool VerifyPassword(this string password, string hashedPassword)
        {
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(hashedPassword))
            {
                throw new ArgumentException("A senha ou o hash não podem estar vazios ou nulos.");
            }

            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
