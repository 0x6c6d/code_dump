namespace Backend.Features.Settings.Operations.Read;

public class ReadSettingsReturn
{
    public Guid Id { get; set; }
    public required int Port { get; set; }
    public required string DefaultCommand { get; set; }
    public required string BufferSizeReceive { get; set; }
}