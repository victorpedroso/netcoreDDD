using TechSolutions.Application.Interfaces;
using TechSolutions.Application.Mappings;
using TechSolutions.Application.Models;
using TechSolutions.Domain.Interfaces;

namespace TechSolutions.Application.Services;

public class RoomService(IRoomRepository roomRepository) : IRoomService
{
    public async Task<Result<IEnumerable<RoomModel>>> GetAll()
    {
        var roomsList = await roomRepository.GetAll();

        if (roomsList == null) return Result<IEnumerable<RoomModel>>.IsError("Nenhuma sala encontrada");

        return Result<IEnumerable<RoomModel>>.IsSuccess(roomsList.ToModelList());
    }

    public async Task<Result<RoomModel>> Get(int id)
    {
        var room = await roomRepository.GetById(id);

        if (room == null) return Result<RoomModel>.IsError("Sala não encontrada");

        return Result<RoomModel>.IsSuccess(room.ToModel());
    }
    public async Task<Result<RoomModel>> Add(RoomModel room)
    {
        var roomCreated = await roomRepository.Add(room.ToEntity());

        return Result<RoomModel>.IsSuccess(roomCreated.ToModel());
    }

    public async Task<Result<bool>> Update(RoomModel room)
    {
        await roomRepository.Update(room.ToEntity());

        return Result<bool>.IsSuccess(true);
    }

    public async Task<Result<bool>> Delete(int id)
    {
        var room = await roomRepository.GetById(id);

        if (room == null) return Result<bool>.IsError("Sala não encontrada");

        await roomRepository.Delete(room);

        return Result<bool>.IsSuccess(true);
    }
}
