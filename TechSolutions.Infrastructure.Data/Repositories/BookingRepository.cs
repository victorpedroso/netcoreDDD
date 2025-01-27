using Microsoft.EntityFrameworkCore;
using TechSolutions.Domain.Entities;
using TechSolutions.Domain.Interfaces;
using TechSolutions.Infrastructure.Data.Context;

namespace TechSolutions.Infrastructure.Data.Repositories;

public class BookingRepository(AppDbContext context) : IBookingRepository
{
    public async Task<IEnumerable<Booking>> GetAll() => await context.Bookings.ToListAsync();
    public async Task<Booking> GetById(Guid id) => await context.Bookings.FindAsync(id);
    public async Task<IEnumerable<Booking>> GetByUserId(Guid id) =>

        await context.Bookings.Where(b => b.UserId == id).ToListAsync();

    public async Task<Booking> Add(Booking booking)
    {
        await context.Bookings.AddAsync(booking);
        await context.SaveChangesAsync();

        return booking;
    }

    public async Task Update(Booking booking)
    {
        context.Entry(booking).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }

    public async Task Delete(Booking booking)
    {
        context.Bookings.Remove(booking);
        await context.SaveChangesAsync();
    }
}
