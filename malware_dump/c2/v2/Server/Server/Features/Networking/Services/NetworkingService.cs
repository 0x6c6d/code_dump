using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Backend.Features.Client.Services;
using Backend.Features.Command.Services;

namespace Server.Features.Networking.Services;

public class NetworkingService(IClientService clientService, ICommandService commandService) : INetworkingService
{
    public async Task HandleNewConnection(TcpClient client, int clientId, ConcurrentDictionary<int, TcpClient> clients, string command, int bufferSize)
    {
        var buffer = new byte[bufferSize];
        var stream = client.GetStream();

        try
        {
            if (client.Client.RemoteEndPoint is not IPEndPoint ipEndPoint)
            {
                clients.TryRemove(clientId, out _);
                client.Close();
                Console.WriteLine("[w] Client IPEndPoint was null");
                return;
            }
            
            var ip = ipEndPoint.Address;
            var port = ipEndPoint.Port;
            var result = await clientService.GetClientByIpAsync(ip.ToString());
            
            byte[] send;
            // handle case when client is new
            if (result.Data == null || !result.Success)
            {
                send = Encoding.UTF8.GetBytes(command);
                Console.WriteLine($"[i] New connection from client {client.Client.RemoteEndPoint}");

                await clientService.CreateClientAsync(ip.ToString(), port, Encoding.ASCII.GetString(send));
            }
            // handle case when client already connected to the server in the past
            else
            {
                command = result.Data.Command;
                send = Encoding.ASCII.GetBytes(command);
                Console.WriteLine($"[i] Client {client.Client.RemoteEndPoint} already connected in the past");

                var clientUpdate = result.Data;
                clientUpdate.Port = port;
                clientUpdate.LastConnection = DateTime.UtcNow;
                await clientService.UpdateClientAsync(clientUpdate);
            }
            
            await stream.WriteAsync(send);
            
            int bytesRead;
            var response = string.Empty;
            while ((bytesRead = await stream.ReadAsync(buffer)) > 0)
            {
                var data = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                response += data;

                if (response.Contains($"\0")) break;
            }
            
            await commandService.CreateCommandAsync(ip.ToString(), command, response);
            Console.WriteLine($"[i] Message from client {client.Client.RemoteEndPoint}: {response}");
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("[w] HandleNewConnection() was canceled");
        }
        catch (Exception e)
        {
            Console.WriteLine($"[e] HandleNewConnection() error message: {e}");
        }
    }
}