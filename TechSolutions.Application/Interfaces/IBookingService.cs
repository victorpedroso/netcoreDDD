using TechSolutions.Application.Models;

namespace TechSolutions.Application.Interfaces;

public interface IBookingService
{
    Task<Result<IEnumerable<BookingModel>>> GetBookings(string userId);
}
