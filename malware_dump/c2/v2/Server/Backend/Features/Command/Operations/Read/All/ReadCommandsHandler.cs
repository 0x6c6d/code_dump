using Backend.Features.Command.Models;
using Backend.Features.Command.Repositories;

namespace Backend.Features.Command.Operations.Read.All;

public class ReadCommandsHandler(ICommandRepository commandRepository) : IRequestHandler<ReadCommandsRequest, List<ReadCommandsReturn>>
{
    public async Task<List<ReadCommandsReturn>> Handle(ReadCommandsRequest request, CancellationToken cancellationToken)
    {
        var clients = (await commandRepository.GetAllAsync()).OrderBy(x => x.Ip).ToList();

        var getClientsReturn = CommandMapper.CommandsToGetCommandsReturn(clients);
        return getClientsReturn;
    }
}