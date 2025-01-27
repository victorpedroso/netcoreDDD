using TechSolutions.Application.Interfaces;
using TechSolutions.Application.Mappings;
using TechSolutions.Application.Models;
using TechSolutions.Domain.Interfaces;

namespace TechSolutions.Application.Services;

public class RoleService(IRoleRepository roleRepository) : IRoleService
{
    public async Task<Result<IEnumerable<RoleModel>>> GetAll()
    {
        var roles = await roleRepository.GetAll();

        return Result<IEnumerable<RoleModel>>.IsSuccess(roles.ToModelList());
    }
    public async Task<Result<RoleModel>> Get(int id)
    {
        var role = await roleRepository.GetById(id);

        if (role == null) return Result<RoleModel>.IsError("Role não encontrada");

        return Result<RoleModel>.IsSuccess(role.ToModel());
    }
    public async Task<Result<RoleModel>> GetByName(string name)
    {
        var role = await roleRepository.GetByName(name);

        if (role == null) return Result<RoleModel>.IsError("Role não encontrada");

        return Result<RoleModel>.IsSuccess(role.ToModel());
    }
    public async Task<Result<RoleModel>> Create(CreateRoleModel roleModel)
    {
        var roleExistis = await roleRepository.GetByName(roleModel.Name);

        if (roleExistis != null) return Result<RoleModel>.IsError("Role já cadastrada");
        var role = roleModel.ToEntity();

        role = await roleRepository.Add(role);

        return Result<RoleModel>.IsSuccess(role.ToModel());
    }

    public async Task<Result<bool>> Delete(int id)
    {
        var role = await roleRepository.GetById(id);

        if (role == null) return Result<bool>.IsError("Role não encontrada");

        await roleRepository.Delete(role);

        return Result<bool>.IsSuccess(true);
    }
}
