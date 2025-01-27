using TechSolutions.Domain.Entities;

namespace TechSolutions.Domain.Interfaces;

public interface IRoomRepository
{
    Task<IEnumerable<Room>> GetAll();
    Task<Room> GetById(int id);
    Task<Room> GetByName(string name);
    Task<Room> Add(Room room);
    Task Update(Room room);
    Task Delete(Room room);
}
