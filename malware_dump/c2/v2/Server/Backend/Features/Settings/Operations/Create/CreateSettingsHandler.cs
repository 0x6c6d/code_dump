using Backend.Features.Settings.Models;
using Backend.Features.Settings.Repositories;

namespace Backend.Features.Settings.Operations.Create;

public class CreateSettingsHandler(ISettingsRepository settingsRepository) : IRequestHandler<CreateSettingsRequest, Guid>
{
    public async Task<Guid> Handle(CreateSettingsRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateSettingsValidator(settingsRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.Errors.Count > 0)
            throw new ValidationException(validationResult.Errors);

        var settings = SettingsMapper.CreateSettingsRequestToSettings(request);
        settings.CreatedDateTime = DateTime.UtcNow;
        settings.LastModifiedDateTime = DateTime.UtcNow;
        settings = await settingsRepository.AddAsync(settings);

        return settings.Id;
    }
}