using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;

namespace Persistence;
public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Default")));

        services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IArticleRepository, ArticleRepository>();
        services.AddScoped<IGroupRepository, GroupRepository>();
        services.AddScoped<IOperationAreaRepository, OperationAreaRepository>();
        services.AddScoped<IProcurementRepository, ProcurementRepository>();
        services.AddScoped<IStoragePlaceRepository, StoragePlaceRepository>();

        return services;
    }
}
