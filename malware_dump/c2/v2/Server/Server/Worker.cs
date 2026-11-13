using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using Backend.Features.Settings.Services;
using Server.Features.Networking.Services;

namespace Server;

public class Worker(IServiceProvider serviceProvider) : BackgroundService
{
    private int _clientId;
    private int _port = 50000;
    private int _bufferSizeReceive = 4096;
    private string _defaultCommand = "whoami";
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine($"[i] Loading settings from database");
        await LoadSettings();
        
        ConcurrentDictionary<int, TcpClient> clients = new();
        TcpListener server = new(IPAddress.Any, _port);
        
        server.Start();
        Console.WriteLine($"[i] Started server on port {_port}");
        
        while (!stoppingToken.IsCancellationRequested)
        {
            if (!server.Pending())
            {
                await LoadSettings();
                await Task.Delay(1000, stoppingToken);
                continue;
            }
            
            var client = await server.AcceptTcpClientAsync(stoppingToken);
            var currentId = _clientId++;
            clients.TryAdd(currentId, client);
            Console.WriteLine($"[i] New connection from {client.Client.RemoteEndPoint}");

            await using var scopeNetwork = serviceProvider.CreateAsyncScope();
            var networkingService = scopeNetwork.ServiceProvider.GetRequiredService<INetworkingService>();
            await Task.Run(() => networkingService.HandleNewConnection(client, currentId, clients, _defaultCommand, _bufferSizeReceive), stoppingToken);
        }
    }

    private async Task LoadSettings()
    {
        await using var scopeSettings = serviceProvider.CreateAsyncScope();
        var settingsService = scopeSettings.ServiceProvider.GetRequiredService<ISettingsService>();
        
        var settingsResult = await settingsService.GetSettingsAsync();
        if (settingsResult is { Success: true, Data: not null })
        {
            _port = settingsResult.Data.Port;
            _defaultCommand = settingsResult.Data.DefaultCommand;
            _bufferSizeReceive = settingsResult.Data.BufferSizeReceive;
        }
    }
}