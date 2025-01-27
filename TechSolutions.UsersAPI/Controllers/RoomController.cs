using Microsoft.AspNetCore.Mvc;
using TechSolutions.Application.Interfaces;
using TechSolutions.Application.Models;
using TechSolutions.UsersAPI.Base;

namespace TechSolutions.UsersAPI.Controllers;


public class RoomController(IRoomService roomService) : BaseController
{
    [HttpGet]
    public async Task<ActionResult<Result<IEnumerable<RoomModel>>>> GetAll()
    {
        return await roomService.GetAll();
    }
    [HttpPost]
    public async Task<ActionResult> Add(RoomModel room)
    {
        var roomCreated = await roomService.Add(room);

        return Ok(roomCreated);
    }
}
