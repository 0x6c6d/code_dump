using Backend.Features.Settings.Models;
using Backend.Features.Settings.Repositories;

namespace Backend.Features.Settings.Operations.Update;

public class UpdateSettingsHandler(ISettingsRepository settingsRepository) : IRequestHandler<UpdateSettingsRequest, Unit>
{
    public async Task<Unit> Handle(UpdateSettingsRequest request, CancellationToken cancellationToken)
    {
        var command = await settingsRepository.GetByIdAsync(request.Id);
        if (command == null)
            throw new NotFoundException(nameof(Settings), request.Id);

        var validator = new UpdateSettingsValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.Errors.Count > 0)
            throw new ValidationException(validationResult.Errors);

        SettingsMapper.UpdateSettingsRequestToSettings(request, command);
        command.LastModifiedDateTime = DateTime.UtcNow;
        await settingsRepository.UpdateAsync(command);

        return Unit.Value;
    }
}