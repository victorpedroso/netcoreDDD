namespace TechSolutions.Application.Models;

public record OperatingHoursModel(int Id, DayOfWeek DayOfWeek, TimeSpan StartTime, TimeSpan EndTime);