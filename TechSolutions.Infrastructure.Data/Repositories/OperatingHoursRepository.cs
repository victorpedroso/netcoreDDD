using Microsoft.EntityFrameworkCore;
using TechSolutions.Domain.Entities;
using TechSolutions.Domain.Interfaces;
using TechSolutions.Infrastructure.Data.Context;

namespace TechSolutions.Infrastructure.Data.Repositories;

public class OperatingHoursRepository(AppDbContext context) : IOperatingHoursRepository
{
    public async Task<IEnumerable<OperatingHours>> GetAll()
    {
        return await context.OperatingHours.ToListAsync();
    }

    public async Task<OperatingHours> Get(int id)
    {
        return await context.OperatingHours.FindAsync(id);
    }
    public async Task<OperatingHours> Add(OperatingHours operatingHours)
    {
        await context.OperatingHours.AddAsync(operatingHours);
        await context.SaveChangesAsync();

        return operatingHours;
    }

    public async Task Update(OperatingHours operatingHours)
    {
        context.Entry(operatingHours).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }

    public async Task Delete(OperatingHours operatingHours)
    {
        context.Remove(operatingHours);
        await context.SaveChangesAsync();
    }
}
