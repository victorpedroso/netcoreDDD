using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using System.Text.Json;
using TechSolutions.Application.Interfaces;
using TechSolutions.Application.Models;

namespace TechSolutions.Application.Services;

public class RedisService<T>(IDistributedCache redisCache) : IRedisService<T> where T : class
{
    public async Task<Result<T>> Get(string key)
    {
        var result = await redisCache.GetStringAsync(key);

        if (string.IsNullOrEmpty(result)) return Result<T>.IsError("Registro não encontrado");

        var response = JsonSerializer.Deserialize<T>(result);

        return Result<T>.IsSuccess(response);
    }
    public async Task<Result<T>> Update(string key, T entity, TimeSpan? expiration = null)
    {
        var options = new DistributedCacheEntryOptions();
        if (expiration.HasValue)
        {
            options.AbsoluteExpirationRelativeToNow = expiration;
        }

        await redisCache.SetStringAsync(key, JsonSerializer.Serialize(entity), options);

        return await Get(key);
    }
    public async Task Delete(string key)
    {
        await redisCache.RemoveAsync(key);
    }
}
