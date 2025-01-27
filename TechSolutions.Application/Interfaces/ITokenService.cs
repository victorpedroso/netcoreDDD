using TechSolutions.Application.Models;
using TechSolutions.Domain.Entities;

namespace TechSolutions.Application.Interfaces;

public interface ITokenService
{
    Task<TokenModel> GenerateToken(User user);
    Task<Token> GetToken(string refreshToken);
}
