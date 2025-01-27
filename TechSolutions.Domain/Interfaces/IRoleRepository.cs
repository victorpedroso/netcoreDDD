using TechSolutions.Domain.Entities;

namespace TechSolutions.Domain.Interfaces;

public interface IRoleRepository
{
    Task<IEnumerable<Role>> GetAll();
    Task<Role> GetById(int id);
    Task<Role> GetByName(string name);
    Task<Role> Add(Role role);
    Task Delete(Role role);
}
