using Backend.Common.Persistence;
using Backend.Common.Persistence.Repositories;
using Backend.Features.Command.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.Command.Repositories;

public class CommandRepository(DataContext dbContext) : BaseRepository<CommandModel>(dbContext), ICommandRepository
{
    private readonly DataContext _dbContext = dbContext;

    public async Task<List<CommandModel>?> GetCommandsByIp(string ip)
    {
        var commands = await _dbContext.Commands.Where(command => command.Ip.ToLower() == ip.ToLower())
            .OrderByDescending(command => command.ExecutionDateTime).ToListAsync();
        return commands;
    }
}