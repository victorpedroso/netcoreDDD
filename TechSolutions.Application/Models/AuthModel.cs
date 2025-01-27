namespace TechSolutions.Application.Models;

public record AuthModel(string Email, string Password);
public record TokenModel(string AccessToken, long ExpiresAccessToken, string RefreshToken, long ExpiresRefreshToken);
