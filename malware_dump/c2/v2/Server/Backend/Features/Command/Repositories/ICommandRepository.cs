using Backend.Common.Persistence.Repositories;
using Backend.Features.Command.Models;

namespace Backend.Features.Command.Repositories;

public interface ICommandRepository : IBaseRepository<CommandModel>
{
    Task<List<CommandModel>?> GetCommandsByIp(string ip);
}