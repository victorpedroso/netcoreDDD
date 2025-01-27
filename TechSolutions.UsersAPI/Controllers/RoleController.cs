using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechSolutions.Application.Interfaces;
using TechSolutions.Application.Models;
using TechSolutions.UsersAPI.Base;

namespace TechSolutions.UsersAPI.Controllers;

[Authorize(Policy = "Admin")]
public class RoleController(IRoleService roleService) : BaseController
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<RoleModel>>> Create(CreateRoleModel roleModel)
    {
        var role = await roleService.Create(roleModel);

        return Ok(role);

    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<bool>>> Delete(int id)
    {
        var role = await roleService.Delete(id);

        return Ok(role);
    }
}
