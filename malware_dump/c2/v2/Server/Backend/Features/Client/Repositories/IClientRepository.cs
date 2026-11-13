using Backend.Common.Persistence.Repositories;
using Backend.Features.Client.Models;

namespace Backend.Features.Client.Repositories;

public interface IClientRepository : IBaseRepository<ClientModel>
{
    Task<ClientModel?> GetClientByIpAsync(string ip);
    Task<bool> IsClientIpUnique(string ip);
}