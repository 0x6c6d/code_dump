using Backend.Common.Persistence;
using Backend.Common.Persistence.Repositories;
using Backend.Features.Client.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.Client.Repositories;

public class ClientRepository(DataContext dbContext) : BaseRepository<ClientModel>(dbContext), IClientRepository
{
    private readonly DataContext _dbContext = dbContext;

    public async Task<ClientModel?> GetClientByIpAsync(string ip)
    {
        var client = await _dbContext.Clients.FirstOrDefaultAsync(client => client.Ip.ToLower() == ip.ToLower());
        return client;
    }
    
    public async Task<bool> IsClientIpUnique(string ip)
    {
        var match = await _dbContext.Clients.AnyAsync(client => client.Ip.ToLower() == ip.ToLower());
        return match;
    }
}