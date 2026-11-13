using System.Collections.Concurrent;
using System.Net.Sockets;

namespace Server.Features.Networking.Services;

public interface INetworkingService
{
    Task HandleNewConnection(TcpClient client, int clientId, ConcurrentDictionary<int, TcpClient> clients, string command, int bufferSize);
}