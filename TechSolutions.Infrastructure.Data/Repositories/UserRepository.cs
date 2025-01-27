using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TechSolutions.Domain.Entities;
using TechSolutions.Domain.Interfaces;
using TechSolutions.Infrastructure.Data.Context;

namespace TechSolutions.Infrastructure.Data.Repositories;

public class UserRepository(AppDbContext context) : IUserRepository
{
    public async Task<IEnumerable<User>> GetAll(Expression<Func<User, bool>> filter = null)
    {
        if (filter == null) return await context.Users.Include(u => u.Role).ToListAsync();

        return await context.Users.Where(filter).ToListAsync();
    }

    public async Task<User> GetById(Guid id)
    {
        return await context.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User> GetByEmail(string email)
    {
        return await context.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User> GetByCpf(string cpf)
    {
        return await context.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Cpf == cpf);
    }

    public async Task<User> Add(User user)
    {
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();

        return user;
    }

    public async Task Update(User user)
    {
        context.Users.Update(user);
        await context.SaveChangesAsync();
    }

    public async Task Delete(User user)
    {
        context.Users.Remove(user);
        await context.SaveChangesAsync();
    }
}
