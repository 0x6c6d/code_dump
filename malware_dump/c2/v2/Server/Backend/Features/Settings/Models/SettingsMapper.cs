using Backend.Features.Settings.Operations.Create;
using Backend.Features.Settings.Operations.Read;
using Backend.Features.Settings.Operations.Update;

namespace Backend.Features.Settings.Models;

[Mapper]
public static partial class SettingsMapper
{
    // Operations
    public static partial ReadSettingsReturn SettingsToReadSettingsReturn(SettingsModel model);
    public static partial SettingsModel CreateSettingsRequestToSettings(CreateSettingsRequest model);
    
    public static partial void UpdateSettingsRequestToSettings(UpdateSettingsRequest request, SettingsModel model);
    
    // Services
    public static partial SettingsModel ReadSettingsReturnToSettings(ReadSettingsReturn model);
    
    //public static partial List<CommandModel> GetCommandsByIpReturnToCommands(List<ReadCommandsByIpReturn> model);
    // 
    // public static partial List<CommandModel> GetCommandsReturnToCommands(List<ReadCommandsReturn> model);
}