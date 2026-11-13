using Backend.Common.Exceptions;
using Backend.Features.Command.Models;
using Backend.Features.Command.Repositories;

namespace Backend.Features.Command.Operations.Read.PerClient;

public class ReadCommandsPerIpHandler(ICommandRepository commandRepository) : IRequestHandler<ReadCommandsPerIpRequest, List<ReadCommandsByIpReturn>>
{
    public async Task<List<ReadCommandsByIpReturn>> Handle(ReadCommandsPerIpRequest request, CancellationToken cancellationToken)
    {
        var client = await commandRepository.GetCommandsByIp(request.Ip);
        if (client == null)
        {
            throw new NotFoundException(nameof(CommandModel), request.Ip);
        }

        var getClientReturn = CommandMapper.CommandsToGetCommandsPerIpReturn(client);
        return getClientReturn;
    }
}