using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TechSolutions.Application.Helper;
using TechSolutions.Application.Interfaces;
using TechSolutions.Application.Services;
using TechSolutions.Domain.Interfaces;
using TechSolutions.Infrastructure.Data.Context;
using TechSolutions.Infrastructure.Data.Repositories;

namespace TechSolutions.Infrastructure.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddContext(services, configuration);
        AddRepositories(services);
        AddServices(services);

        return services;
    }
    private static IServiceCollection AddContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("Default"),
                m => m.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));
        });

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration["CacheSettings:ConnectionString"];
        });

        return services;
    }


    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<ITokenRepository, TokenRepository>();
        services.AddScoped<IRoomRepository, RoomRepository>();
        services.AddScoped<IBookingRepository, BookingRepository>();
        services.AddScoped<IOperatingHoursRepository, OperatingHoursRepository>();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IRoomService, RoomService>();
        services.AddScoped(typeof(IRedisService<>), typeof(RedisService<>));

        services.AddScoped<CryptHelper>();

        return services;
    }
}
