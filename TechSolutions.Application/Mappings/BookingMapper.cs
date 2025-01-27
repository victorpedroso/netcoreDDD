using TechSolutions.Application.Models;
using TechSolutions.Application.Models.Request;
using TechSolutions.Domain.Entities;

namespace TechSolutions.Application.Mappings;

public static class BookingMapper
{
    public static BookingModel ToModel(this Booking booking) =>

         new(booking.RoomId, booking.UserId, booking.StartAt, booking.EndAt, booking.Price);
    

    public static BookingModel ToModel(this BookingRequestModel booking, Guid userId) =>

         new(booking.RoomId, userId, booking.StartAt, booking.EndAt, booking.Price);
}
