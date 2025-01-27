using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using TechSolutions.Application.Interfaces;
using TechSolutions.Application.Models;
using TechSolutions.UsersAPI.Base;

namespace TechSolutions.UsersAPI.Controllers;

[Authorize]
public class AccountController(IUserService userService) : BaseController
{
    [HttpGet("me-info")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<UserModel>>> MeInfo()
    {
        var userId = User?.FindFirst(JwtRegisteredClaimNames.Sid)?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        var user = await userService.MeInfo(userId);

        return Ok(user);
    }

    [AllowAnonymous]
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<TokenModel>>> Register(CreateUserModel user)
    {
        var userCreated = await userService.Create(user);

        if (userCreated.Success)
        {
            var userAuth = await userService.Auth(new AuthModel(user.Email, user.Password));

            return Ok(userAuth);
        }

        return Ok(userCreated);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<TokenModel>>> Login(AuthModel auth)
    {
        var authentication = await userService.Auth(auth);

        return Ok(authentication);

    }

    [AllowAnonymous]
    [HttpPut("reauth")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<TokenModel>>> ReAuth([FromQuery]  string refreshToken)
    {
        var reAuthentication = await userService.ReAuth(refreshToken);

        return Ok(reAuthentication);
    }

    [AllowAnonymous]
    [HttpPut("forgot-password")]
    public async Task<ActionResult> ForgotPassword([FromQuery] string email)
    {
        var result = await userService.ForgotPassword(email);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPut("change-password")]
    public async Task<ActionResult> ChangePassword(ChangePasswordModel model)
    {
        var result = await userService.ChangePassword(model);

        return Ok(result);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<bool>>> Update(CreateUserModel userModel)
    {
        var userId = User?.FindFirst(JwtRegisteredClaimNames.Sid)?.Value;

        var userResult = await userService.Update(userId, userModel);

        return Ok(userResult);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<bool>>> Delete()
    {
        var userId = User?.FindFirst(JwtRegisteredClaimNames.Sid)?.Value;

        var userResult = await userService.Delete(userId);

        return Ok(userResult);
    }
}
