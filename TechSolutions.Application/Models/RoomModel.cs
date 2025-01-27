namespace TechSolutions.Application.Models;

public record RoomModel(int Id, string Name, IEnumerable<OperatingHoursModel> OperatingHours);
