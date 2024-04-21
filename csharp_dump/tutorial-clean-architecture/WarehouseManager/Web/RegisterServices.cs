namespace Web;

public static class RegisterServices
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        // Add services to the container.
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        builder.Services.AddHttpClient();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<IArticleService, ArticleService>();
        builder.Services.AddScoped<IDataAccessArticleService, DataAccessArticleService>();
        builder.Services.AddScoped<IProcurementService, ProcurementService>();
        builder.Services.AddScoped<IDataAccessProcurementService, DataAccessProcurementService>();
        builder.Services.AddScoped(typeof(IEntityService<>), typeof(EntityService<>));
        builder.Services.AddScoped(typeof(IDataAccessEntityService<>), typeof(DataAccessEntityService<>));
        builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("Settings"));
    }
}