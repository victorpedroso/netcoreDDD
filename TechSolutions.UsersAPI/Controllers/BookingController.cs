using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using TechSolutions.UsersAPI.Base;

namespace TechSolutions.UsersAPI.Controllers;

[Authorize]
public class BookingController : BaseController
{
    [HttpGet("me")]
    public async Task<ActionResult> Me()
    {
        var userId = User?.FindFirst(JwtRegisteredClaimNames.Sid)?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult> Add()
    {
        return Ok();
    }

    [HttpPut]
    public async Task<ActionResult> Update()
    {
        return Ok();
    }

    [HttpDelete]
    public async Task<ActionResult> Delete()
    {
        return Ok();
    }
}
