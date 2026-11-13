using Backend.Features.Client.Operations.Create;
using Backend.Features.Client.Operations.Read.All;
using Backend.Features.Client.Operations.Read.One;
using Backend.Features.Client.Operations.Update;

namespace Backend.Features.Client.Models;

[Mapper]
public static partial class ClientMapper
{
    // Operations
    public static partial ReadClientReturn ClientToGetClientReturn(ClientModel model);
    public static partial List<ReadClientsReturn> ClientsToGetClientsReturn(List<ClientModel> model);
    public static partial ClientModel CreateClientRequestToClient(CreateClientRequest model);
    public static partial void UpdateClientRequestToClient(UpdateClientRequest request, ClientModel model);
    
    // Services
    public static partial ClientModel ReadClientReturnToClient(ReadClientReturn model);
    public static partial List<ClientModel> ReadClientsReturnToClients(List<ReadClientsReturn> model);
}   