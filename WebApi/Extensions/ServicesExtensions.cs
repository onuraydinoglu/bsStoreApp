using Microsoft.EntityFrameworkCore;
using Presentaiton.ActionFilter;
using Repositories.Contracts;
using Repositories.EFCore;
using Services;
using Services.Contracts;

namespace WebApi.Extensions;

public static class ServicesExtensions
{
    public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<RepositoryContext>(options =>
        {
            var config = configuration;
            var connectionString = config.GetConnectionString("sqlConnection");
            options.UseSqlServer(connectionString);
        });
    }

    public static void ConfigureRepositoryManager(this IServiceCollection services)
    {
        services.AddScoped<IRepositoryManager, RepositoryManager>();
    }

    public static void ConfigureServiceManager(this IServiceCollection services)
    {
        services.AddScoped<IServiceManager, ServiceManager>();
    }

    public static void ConfigureLoggerService(this IServiceCollection services)
    {
        services.AddSingleton<ILoggerService, LoggerManager>();
    }

    public static void ConfigureActionFilter(this IServiceCollection services)
    {
        services.AddScoped<ValidationFilterAttribute>();
        services.AddSingleton<LogFilterAttribute>();
    }
}
