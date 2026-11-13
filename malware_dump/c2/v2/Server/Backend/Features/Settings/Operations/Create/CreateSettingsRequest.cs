namespace Backend.Features.Settings.Operations.Create;

public class CreateSettingsRequest : IRequest<Guid>
{
    public required int Port { get; set; }
    public required string DefaultCommand { get; set; }
    public required int BufferSizeReceive { get; set; }
}