using Backend.Common.Exceptions;
using Backend.Features.Client.Models;
using Backend.Features.Client.Repositories;
using ClientMapper = Backend.Features.Client.Models.ClientMapper;

namespace Backend.Features.Client.Operations.Read.One;

public class ReadClientHandler(IClientRepository clientRepository) : IRequestHandler<ReadClientRequest, ReadClientReturn>
{
    public async Task<ReadClientReturn> Handle(ReadClientRequest request, CancellationToken cancellationToken)
    {
        var client = await clientRepository.GetClientByIpAsync(request.Ip);
        if (client == null)
        {
            throw new NotFoundException(nameof(ClientModel), request.Ip);
        }

        var getClientReturn = ClientMapper.ClientToGetClientReturn(client);
        return getClientReturn;
    }
}