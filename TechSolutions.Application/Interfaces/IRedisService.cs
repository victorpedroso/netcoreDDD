using TechSolutions.Application.Models;

namespace TechSolutions.Application.Interfaces;

public interface IRedisService<T> where T : class
{
    Task<Result<T>> Get(string key);
    Task<Result<T>> Update(string key, T entity, TimeSpan? expiration = null);
    Task Delete(string key);
}
