using TechSolutions.Application.Models;
using TechSolutions.Domain.Entities;

namespace TechSolutions.Application.Mappings;

public static class RoomMapper
{
    public static RoomModel ToModel(this Room room) => new(room.Id, room.Name, room.OperatingHours.ToModelList());

    public static Room ToEntity(this RoomModel room) => new(room.Name, room.OperatingHours.ToEntityList());

    public static IEnumerable<RoomModel> ToModelList(this IEnumerable<Room> roomList) =>

        roomList.Select(ToModel).ToList();

    public static IEnumerable<Room> ToEntityList(this IEnumerable<RoomModel> roomList) =>

        roomList.Select(ToEntity).ToList();   
}
