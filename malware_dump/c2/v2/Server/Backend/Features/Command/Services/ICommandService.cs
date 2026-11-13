using Backend.Common.Models;
using Backend.Features.Command.Models;

namespace Backend.Features.Command.Services;

public interface ICommandService
{
    Task<ServiceResponse<List<CommandModel>?>> GetCommandsAsync();
    Task<ServiceResponse<List<CommandModel>?>> GetCommandPerClientAsync(string ip);
    Task<ServiceResponse<CommandModel>> GetCommandByIdAsync(Guid id);
    Task<ServiceResponse<Guid>> CreateCommandAsync(string ip, string command, string response);
    Task<ServiceResponse<bool>> UpdateCommandAsync(CommandModel command);
    Task<ServiceResponse<bool>> DeleteCommandByIdAsync(Guid id);
    Task<ServiceResponse<bool>> DeleteCommandByIpAsync(string ip);
}