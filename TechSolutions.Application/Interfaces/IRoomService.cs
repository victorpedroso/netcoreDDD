using TechSolutions.Application.Models;

namespace TechSolutions.Application.Interfaces;

public interface IRoomService
{
    Task<Result<IEnumerable<RoomModel>>> GetAll();
    Task<Result<RoomModel>> Get(int id);
    Task<Result<RoomModel>> Add(RoomModel room);
    Task<Result<bool>> Update(RoomModel room);
    Task<Result<bool>> Delete(int id);
}
