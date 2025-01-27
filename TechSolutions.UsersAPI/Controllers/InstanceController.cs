using Microsoft.AspNetCore.Mvc;
using TechSolutions.UsersAPI.Base;

namespace TechSolutions.UsersAPI.Controllers;

public class InstanceController : BaseController
{
    private readonly IHttpClientFactory _httpClientFactory;

    public InstanceController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet("info")]
    public async Task<IActionResult> GetInstanceInfo()
    {
        try
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetStringAsync("https://api.ipify.org");
            var publicIp = response;

            var currentTime = DateTime.UtcNow;

            return Ok(new
            {
                PublicIp = publicIp,
                CurrentTime = currentTime.ToString("o")
            });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                Message = "An error occurred while fetching the instance info.",
                Details = ex.Message
            });
        }
    }

}
