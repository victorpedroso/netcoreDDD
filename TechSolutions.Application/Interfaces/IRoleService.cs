using TechSolutions.Application.Models;

namespace TechSolutions.Application.Interfaces;

public interface IRoleService
{
    Task<Result<IEnumerable<RoleModel>>> GetAll();
    Task<Result<RoleModel>> Get(int id);
    Task<Result<RoleModel>> GetByName(string name);
    Task<Result<RoleModel>> Create(CreateRoleModel roleModel);
    Task<Result<bool>> Delete(int id);
}
