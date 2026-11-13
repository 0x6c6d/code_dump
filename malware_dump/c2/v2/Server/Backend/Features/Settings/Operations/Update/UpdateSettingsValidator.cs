namespace Backend.Features.Settings.Operations.Update;

public class UpdateSettingsValidator : AbstractValidator<UpdateSettingsRequest>
{
    public UpdateSettingsValidator()
    {
        RuleFor(x => x.Port)
            .GreaterThan(1024)
            .LessThan(65536)
            .WithMessage("Port must be between 1024 and 65536");
        
        RuleFor(x => x.BufferSizeReceive)
            .GreaterThan(1024)
            .LessThan(16384)
            .WithMessage("Port must be between 1024 and 16384");
    }
}