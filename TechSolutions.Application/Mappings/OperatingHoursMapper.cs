using TechSolutions.Application.Models;
using TechSolutions.Domain.Entities;

namespace TechSolutions.Application.Mappings;

public static class OperatingHoursMapper
{
    public static OperatingHoursModel ToModel(this OperatingHours operatingHours) =>

        new(operatingHours.Id, operatingHours.DayOfWeek, operatingHours.StartTime, operatingHours.EndTime);

    public static OperatingHours ToEntity(this OperatingHoursModel operatingHours) =>

        new(operatingHours.DayOfWeek, operatingHours.StartTime, operatingHours.EndTime);

    public static IEnumerable<OperatingHoursModel> ToModelList(this IEnumerable<OperatingHours> operatingHours) =>

        operatingHours.Select(ToModel).ToList();

    public static IEnumerable<OperatingHours> ToEntityList(this IEnumerable<OperatingHoursModel> operatingHours) =>

        operatingHours.Select(ToEntity).ToList();
}
