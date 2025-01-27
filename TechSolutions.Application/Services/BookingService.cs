using TechSolutions.Application.Interfaces;
using TechSolutions.Application.Models;

namespace TechSolutions.Application.Services;

public class BookingService : IBookingService
{
    public Task<Result<IEnumerable<BookingModel>>> GetBookings(string userId)
    {
        throw new NotImplementedException();
    }
}
