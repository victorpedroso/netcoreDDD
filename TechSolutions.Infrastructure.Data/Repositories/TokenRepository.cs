using Microsoft.EntityFrameworkCore;
using TechSolutions.Domain.Entities;
using TechSolutions.Domain.Interfaces;
using TechSolutions.Infrastructure.Data.Context;

namespace TechSolutions.Infrastructure.Data.Repositories;

public class TokenRepository(AppDbContext context) : ITokenRepository
{
    public async Task<Token> Get(string refreshToken)
    {
        return await context.Tokens.FirstOrDefaultAsync(t => t.RefreshToken == refreshToken);
    }

    public async Task<Token> GetByUserId(Guid userId)
    {
        return await context.Tokens.Include(t => t.User).FirstOrDefaultAsync(t => t.UserId == userId);
    }

    public async Task Add(Token token)
    {
        var tokenExists = await GetByUserId(token.UserId);

        if (tokenExists != null) await Delete(tokenExists);

        await context.Tokens.AddAsync(token);
        await context.SaveChangesAsync();
    }

    public async Task Update(Token token)
    {
        context.Entry(token).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }

    public async Task Delete(Token token)
    {
        context.Tokens.Remove(token);
        await context.SaveChangesAsync();
    }
}
