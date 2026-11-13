using Backend.Features.Settings.Models;
using Backend.Features.Settings.Repositories;

namespace Backend.Features.Settings.Operations.Read;

public class ReadSettingsHandler(ISettingsRepository settingsRepository) : IRequestHandler<ReadSettingsRequest, ReadSettingsReturn?>
{
    public async Task<ReadSettingsReturn?> Handle(ReadSettingsRequest request, CancellationToken cancellationToken)
    {
        var setting = await settingsRepository.GetFirstOnlyAsync();
        if (setting == null) return null;

        var readSettingsReturn = SettingsMapper.SettingsToReadSettingsReturn(setting);
        return readSettingsReturn;
    }
}