using TechSolutions.Application.Models;
using TechSolutions.Domain.Entities;

namespace TechSolutions.Application.Mappings;

public static class RoleMapper
{
    public static RoleModel ToModel(this Role role) => new(role.Id, role.Name);
    public static Role ToEntity(this CreateRoleModel role) => new(role.Name);
    public static Role ToEntity(this RoleModel roleDto) => new(roleDto.Name);
    public static IEnumerable<RoleModel> ToModelList(this IEnumerable<Role> roles) =>

        roles.Select(ToModel);

    public static IEnumerable<Role> ToEntityList(this IEnumerable<RoleModel> roleDtos) =>

        roleDtos.Select(ToEntity);
}
