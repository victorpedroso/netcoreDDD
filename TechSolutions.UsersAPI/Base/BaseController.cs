using Microsoft.AspNetCore.Mvc;

namespace TechSolutions.UsersAPI.Base;

[Route("api/[controller]")]
[ApiController]
public abstract class BaseController : ControllerBase
{
}
