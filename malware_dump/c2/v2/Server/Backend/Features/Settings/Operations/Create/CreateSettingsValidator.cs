using Backend.Features.Settings.Repositories;

namespace Backend.Features.Settings.Operations.Create;

public class CreateSettingsValidator : AbstractValidator<CreateSettingsRequest>
{
    private readonly ISettingsRepository _settingsRepository;

    public CreateSettingsValidator(ISettingsRepository settingsRepository)
    {
        _settingsRepository = settingsRepository;
        
        RuleFor(x => x.Port)
            .GreaterThan(1024)
            .LessThan(65536)
            .WithMessage("Port must be between 1024 and 65536");
        
        RuleFor(x => x.BufferSizeReceive)
            .GreaterThan(1024)
            .LessThan(16384)
            .WithMessage("Port must be between 1024 and 16384");
        
        RuleFor(x => x)
            .MustAsync(IsTableEmpty)
            .WithMessage("An entry in the settings table is already present.");
    }
    
    private async Task<bool> IsTableEmpty(CreateSettingsRequest e, CancellationToken token)
    {
        return !await _settingsRepository.IsTableEmpty();
    }
}