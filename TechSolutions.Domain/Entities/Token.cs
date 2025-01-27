namespace TechSolutions.Domain.Entities;

public sealed class Token
{
    public Guid Id { get; init; }
    public string? RefreshToken { get; private set; }
    public long Expires { get; private set; }
    public Guid UserId { get; private set; }
    public User? User { get; private set; }

    public Token(string refreshToken, long expires, Guid userId)
    {
        Id = Guid.NewGuid();
        RefreshToken = refreshToken;
        Expires = expires;
        UserId = userId;
    }
}
