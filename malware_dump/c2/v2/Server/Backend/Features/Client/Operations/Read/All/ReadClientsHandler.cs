using Backend.Features.Client.Repositories;
using ClientMapper = Backend.Features.Client.Models.ClientMapper;

namespace Backend.Features.Client.Operations.Read.All;

public class ReadClientsHandler(IClientRepository clientRepository) : IRequestHandler<ReadClientsRequest, List<ReadClientsReturn>>
{
    public async Task<List<ReadClientsReturn>> Handle(ReadClientsRequest request, CancellationToken cancellationToken)
    {
        var clients = (await clientRepository.GetAllAsync()).OrderBy(x => x.Ip).ToList();

        var getClientsReturn = ClientMapper.ClientsToGetClientsReturn(clients);
        return getClientsReturn;
    }
}