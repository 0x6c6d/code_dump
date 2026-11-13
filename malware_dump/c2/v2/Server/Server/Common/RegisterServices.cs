using Backend.Common.Persistence;
using Backend.Features.Client.Repositories;
using Backend.Features.Client.Services;
using Backend.Features.Command.Repositories;
using Backend.Features.Command.Services;
using Backend.Features.Settings.Repositories;
using Backend.Features.Settings.Services;
using Microsoft.EntityFrameworkCore;
using Server.Features.Networking.Services;

namespace Server.Common;

public static class RegisterServices
{
    public static void ConfigureServices(this HostApplicationBuilder builder)
    {
        // default stuff
        builder.Services.AddHostedService<Worker>();
        
        // services - Server
        builder.Services.AddScoped<INetworkingService, NetworkingService>();
        
        // services - Backend
        builder.Services.AddScoped<IClientRepository, ClientRepository>();
        builder.Services.AddScoped<ICommandRepository, CommandRepository>();
        builder.Services.AddScoped<ISettingsRepository, SettingsRepository>();
        
        builder.Services.AddScoped<IClientService, ClientService>();
        builder.Services.AddScoped<ICommandService, CommandService>();
        builder.Services.AddScoped<ISettingsService, SettingsService>();
        
        // database
        // TODO: get connection string from config
        builder.Services.AddDbContext<DataContext>(options =>
            options.UseSqlite("Data Source=/home/lucas/workspace/code/db/c2.sqlite"));
        
        // MediatR: ensures that MediatR searches for handlers within the class library "Backend"
        const string classLibraryName = "Backend";
        var assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.GetName().Name == classLibraryName);
        builder.Services.AddMediatR(configuration =>
        {
            if (assembly != null) configuration.RegisterServicesFromAssembly(assembly);
        });
    }
}