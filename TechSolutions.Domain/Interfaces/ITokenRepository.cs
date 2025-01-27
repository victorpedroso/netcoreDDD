using TechSolutions.Domain.Entities;

namespace TechSolutions.Domain.Interfaces;

public interface ITokenRepository
{
    Task<Token> Get(string refreshToken);
    Task<Token> GetByUserId(Guid userId);
    Task Add(Token token);
    Task Update(Token token);
    Task Delete(Token token);
}
