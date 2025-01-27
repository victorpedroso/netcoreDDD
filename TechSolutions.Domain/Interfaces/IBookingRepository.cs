using TechSolutions.Domain.Entities;

namespace TechSolutions.Domain.Interfaces;

public interface IBookingRepository
{
    Task<IEnumerable<Booking>> GetAll();
    Task<Booking> GetById(Guid id);
    Task<IEnumerable<Booking>> GetByUserId(Guid id);
    Task<Booking> Add(Booking booking);
    Task Update(Booking booking);
    Task Delete(Booking booking);
}
