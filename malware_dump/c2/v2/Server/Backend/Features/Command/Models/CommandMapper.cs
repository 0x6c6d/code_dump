using Backend.Features.Command.Operations.Create;
using Backend.Features.Command.Operations.Read.All;
using Backend.Features.Command.Operations.Read.One;
using Backend.Features.Command.Operations.Read.PerClient;
using Backend.Features.Command.Operations.Update;

namespace Backend.Features.Command.Models;

[Mapper]
public static partial class CommandMapper
{
    // Operations
    public static partial CommandModel CreateCommandRequestToCommand(CreateCommandRequest model);
    
    public static partial ReadCommandReturn CommandToGetCommandReturn(CommandModel model);
    
    public static partial List<ReadCommandsByIpReturn> CommandsToGetCommandsPerIpReturn(List<CommandModel> model);
    
    public static partial List<ReadCommandsReturn> CommandsToGetCommandsReturn(List<CommandModel> model);

    public static partial void UpdateCommandRequestToCommand(UpdateCommandRequest request, CommandModel model);

    // Services
    public static partial CommandModel ReadCommandReturnToCommand(ReadCommandReturn model);
    
    public static partial List<CommandModel> ReadCommandsByIpReturnToCommands(List<ReadCommandsByIpReturn> model);
    
    public static partial List<CommandModel> ReadCommandsReturnToCommands(List<ReadCommandsReturn> model);
}