using Microsoft.EntityFrameworkCore;
using TechSolutions.Domain.Entities;
using TechSolutions.Domain.Interfaces;
using TechSolutions.Infrastructure.Data.Context;

namespace TechSolutions.Infrastructure.Data.Repositories;

public class RoomRepository(AppDbContext context) : IRoomRepository
{
    public async Task<IEnumerable<Room>> GetAll()
    {
        return await context.Rooms.ToListAsync();
    }

    public async Task<Room> GetById(int id)
    {
        return await context.Rooms.FindAsync(id);
    }

    public async Task<Room> GetByName(string name)
    {
        return await context.Rooms.FirstOrDefaultAsync(r => r.Name.ToLower() == name.ToLower());
    }
    public async Task<Room> Add(Room room)
    {
        await context.Rooms.AddAsync(room);
        await context.SaveChangesAsync();

        return room;
    }

    public async Task Update(Room room)
    {
        context.Entry(room).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }

    public async Task Delete(Room room)
    {
        context.Rooms.Remove(room);
        await context.SaveChangesAsync();
    }
}
