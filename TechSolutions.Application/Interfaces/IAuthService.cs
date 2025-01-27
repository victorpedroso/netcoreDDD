using TechSolutions.Application.Models;

namespace TechSolutions.Application.Interfaces;

public interface IAuthService
{
    Task<Result<TokenModel>> Auth(AuthModel auth);
    Task<Result<TokenModel>> ReAuth(string refreshToken);
}
