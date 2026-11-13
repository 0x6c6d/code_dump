using Backend.Common.Models;
using Backend.Features.Client.Models;

namespace Backend.Features.Client.Services;

public interface IClientService
{
    Task<ServiceResponse<List<ClientModel>?>> GetClientsAsync();
    Task<ServiceResponse<ClientModel>> GetClientByIpAsync(string ip);
    Task<ServiceResponse<Guid>> CreateClientAsync(string ip, int port, string command);
    Task<ServiceResponse<bool>> UpdateClientAsync(ClientModel client);
    Task<ServiceResponse<bool>> DeleteClientAsync(ClientModel client);
}