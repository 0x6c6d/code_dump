using Backend.Common.Models;
using Backend.Features.Command.Models;
using Backend.Features.Command.Operations.Create;
using Backend.Features.Command.Operations.Delete.ById;
using Backend.Features.Command.Operations.Delete.ByIp;
using Backend.Features.Command.Operations.Read.All;
using Backend.Features.Command.Operations.Read.One;
using Backend.Features.Command.Operations.Read.PerClient;
using Backend.Features.Command.Operations.Update;

namespace Backend.Features.Command.Services;

public class CommandService(IMediator mediator) : ICommandService
{
    public async Task<ServiceResponse<List<CommandModel>?>> GetCommandsAsync()
    {
        try
        {
            var result = await mediator.Send(new ReadCommandsRequest());
            var commands = CommandMapper.ReadCommandsReturnToCommands(result);
            
            return new ServiceResponse<List<CommandModel>?> { Data = commands };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<List<CommandModel>?>
            {
                Success = false,
                Message = ex.Message
            };
        }
    }
    
    public async Task<ServiceResponse<List<CommandModel>?>> GetCommandPerClientAsync(string ip)
    {
        try
        {
            var result = await mediator.Send(new ReadCommandsPerIpRequest() { Ip = ip });
            var command = CommandMapper.ReadCommandsByIpReturnToCommands(result);
            
            return new ServiceResponse<List<CommandModel>?> { Data = command };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<List<CommandModel>?>
            {
                Success = false,
                Message = ex.Message
            };
        }
    }
    
    public async Task<ServiceResponse<CommandModel>> GetCommandByIdAsync(Guid id)
    {
        try
        {
            var result = await mediator.Send(new ReadCommandRequest() { Id = id });
            var command = CommandMapper.ReadCommandReturnToCommand(result);
            
            return new ServiceResponse<CommandModel> { Data = command };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<CommandModel>
            {
                Success = false,
                Message = ex.Message
            };
        }
    }
    
    public async Task<ServiceResponse<Guid>> CreateCommandAsync(string ip, string command, string response)
    {
        try
        {
            var result = await mediator.Send(new CreateCommandRequest() { Ip = ip, Command = command, Response = response });
            if (result == Guid.Empty)
            {
                return new ServiceResponse<Guid>
                {
                    Success = false,
                    Message = "Unable to create command."
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
    
    public async Task<ServiceResponse<bool>> UpdateCommandAsync(CommandModel command)
    {
        try
        {
            await mediator.Send(new UpdateCommandRequest()
            {
                Id = command.Id,
                Ip = command.Ip,
                Command = command.Command,
                Response = command.Response,
                ExecutionDateTime = command.ExecutionDateTime,
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
    
    public async Task<ServiceResponse<bool>> DeleteCommandByIdAsync(Guid id)
    {
        try
        {
            await mediator.Send(new DeleteCommandByIdRequest() { Id = id });
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
    
    public async Task<ServiceResponse<bool>> DeleteCommandByIpAsync(string ip)
    {
        try
        {
            await mediator.Send(new DeleteCommandsByIpRequest() { Ip = ip });
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