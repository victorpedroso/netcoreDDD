using TechSolutions.Domain.Entities;

namespace TechSolutions.Domain.Interfaces;

public interface IOperatingHoursRepository
{
    Task<IEnumerable<OperatingHours>> GetAll();
    Task<OperatingHours> Get(int id);
    Task<OperatingHours> Add(OperatingHours operatingHours);
    Task Update(OperatingHours operatingHours);
    Task Delete(OperatingHours operatingHours);
}
