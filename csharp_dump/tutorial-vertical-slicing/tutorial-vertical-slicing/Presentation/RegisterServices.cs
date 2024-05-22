using Application.Common.Persistence;
using Application.Common.Persistence.Repositories;
using Application.Features.StoreManager.Events.Respositories;
using Application.Features.StoreManager.Events.Services;
using Application.Features.StoreManager.Stores.Repositories;
using Application.Features.StoreManager.Technologies.Repositories;
using Application.Features.WarehouseManager.Articles.Repositories;
using Application.Features.WarehouseManager.Categories.Repositories;
using Application.Features.WarehouseManager.Groups.Repositories;
using Application.Features.WarehouseManager.OperationAreas.Repositories;
using Application.Features.WarehouseManager.Procurements.Repositories;
using Application.Features.WarehouseManager.StoragePlaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Presentation;

public static class RegisterServices
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        // default stuff
        builder.Services.AddHttpClient();
        builder.Services.AddRazorComponents().AddInteractiveServerComponents();

        // MediatR: ensures that MediatR searches for handlers within the class library "Application"
        var classLibraryName = "Application";
        var assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.GetName().Name == classLibraryName)!;
        builder.Services.AddMediatR(configuration =>
            configuration.RegisterServicesFromAssembly(assembly));

        // entity framework
        builder.Services.AddDbContext<DataContext>(
            options =>
                options.UseSqlServer(
                        builder.Configuration.GetConnectionString("Default"),
                        x => x.MigrationsAssembly("Application")),
            ServiceLifetime.Scoped);

        #region Common
        builder.Services.AddScoped<IAxService, AxService>();
        #endregion

        #region Store Kennung
        // repositories
        builder.Services.AddScoped<IStoreRepository, StoreRepository>();
        builder.Services.AddScoped<IEventRepository, EventRepository>();
        builder.Services.AddScoped<IEventTypeRepository, EventTypeRepository>();
        builder.Services.AddScoped<ITechnologyRepository, TechnologyRepository>();

        // services
        builder.Services.AddScoped<IStoreService, StoreService>();
        builder.Services.AddScoped<IEventService, EventService>();
        builder.Services.AddScoped<ITechnologyService, TechnologyService>();
        #endregion

        #region Warehouse Manager
        // repositories
        builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
        builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
        builder.Services.AddScoped<IGroupRepository, GroupRepository>();
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddScoped<IOperationAreaRepository, OperationAreaRepository>();
        builder.Services.AddScoped<IProcurementRepository, ProcurementRepository>();
        builder.Services.AddScoped<IStoragePlaceRepository, StoragePlaceRepository>();

        // services
        builder.Services.AddScoped<IArticleService, ArticleService>();
        builder.Services.AddScoped<IGroupService, GroupService>();
        builder.Services.AddScoped<ICategoryService, CategoryService>();
        builder.Services.AddScoped<IOperationAreaService, OperationAreaService>();
        builder.Services.AddScoped<IProcurementService, ProcurementService>();
        builder.Services.AddScoped<IStoragePlaceService, StoragePlaceService>();     
        #endregion
    }
}
