using Backend.Common.Models;
using Backend.Features.Client.Models;
using Backend.Features.Client.Operations.Create;
using Backend.Features.Client.Operations.Delete;
using Backend.Features.Client.Operations.Read.All;
using Backend.Features.Client.Operations.Read.One;
using Backend.Features.Client.Operations.Update;
using Backend.Features.Command.Services;

namespace Backend.Features.Client.Services;

public class ClientService(IMediator mediator, ICommandService commandService) : IClientService
{
    public async Task<ServiceResponse<List<ClientModel>?>> GetClientsAsync()
    {
        try
        {
            var result = await mediator.Send(new ReadClientsRequest());
            var clients = ClientMapper.ReadClientsReturnToClients(result);
            
            return new ServiceResponse<List<ClientModel>?> { Data = clients };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<List<ClientModel>?>
            {
                Success = false,
                Message = ex.Message
            };
        }
    }
    
    public async Task<ServiceResponse<ClientModel>> GetClientByIpAsync(string ip)
    {
        try
        {
            var result = await mediator.Send(new ReadClientRequest() { Ip = ip });
            var client = ClientMapper.ReadClientReturnToClient(result);
            
            return new ServiceResponse<ClientModel> { Data = client };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<ClientModel>
            {
                Success = false,
                Message = ex.Message
            };
        }
    }
    
    public async Task<ServiceResponse<Guid>> CreateClientAsync(string ip, int port, string command)
    {
        try
        {
            var result = await mediator.Send(new CreateClientRequest() { Ip = ip, Port = port, Command = command});
            if (result == Guid.Empty)
            {
                return new ServiceResponse<Guid>
                {
                    Success = false,
                    Message = "Unable to create client."
                };
            }
            
            return new ServiceResponse<Guid> { Data = result };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<Guid>
            {
                Success = false,
                Message = ex.Message
            };
        }
    }
    
    public async Task<ServiceResponse<bool>> UpdateClientAsync(ClientModel client)
    {
        try
        {
            await mediator.Send(new UpdateClientRequest()
            {
                Id = client.Id,
                Ip = client.Ip,
                Port = client.Port,
                Command = client.Command
            });

            return new ServiceResponse<bool> { Data = true };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<bool>
            {
                Success = false,
                Message = ex.Message
            };
        }
    }
    
    public async Task<ServiceResponse<bool>> DeleteClientAsync(ClientModel client)
    {
        try
        {
            var result = await commandService.DeleteCommandByIpAsync(client.Ip);
            if (!result.Data || !result.Success)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = !string.IsNullOrEmpty(result.Message) ? result.Message : $"Error deleting the commands for the client {client.Ip}"
                };
            }
            
            await mediator.Send(new DeleteClientRequest() { Id = client.Id });
            return new ServiceResponse<bool> { Data = true };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<bool>
            {
                Success = false,
                Message = ex.Message
            };
        }
    }
}