namespace TechSolutions.Application.Models;

public record BookingModel(int RoomId, Guid UserId, long StartAt, long EndAt, decimal? Price);
