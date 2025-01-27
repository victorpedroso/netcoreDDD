using Microsoft.EntityFrameworkCore;
using TechSolutions.Domain.Entities;
using TechSolutions.Domain.Interfaces;
using TechSolutions.Infrastructure.Data.Context;

namespace TechSolutions.Infrastructure.Data.Repositories;

public class RoleRepository(AppDbContext context) : IRoleRepository
{
    public async Task<IEnumerable<Role>> GetAll()
    {
        return await context.Roles.ToListAsync();
    }
    public async Task<Role> GetById(int id)
    {
        return await context.Roles.FindAsync(id);
    }
    public async Task<Role> GetByName(string name)
    {
        return await context.Roles.FirstOrDefaultAsync(r => r.Name.ToLower() == name.ToLower());
    }
    public async Task<Role> Add(Role role)
    {
        await context.Roles.AddAsync(role);
        await context.SaveChangesAsync();

        return role;
    }

    public async Task Delete(Role role)
    {
        context.Roles.Remove(role);
        await context.SaveChangesAsync();
    }
}
