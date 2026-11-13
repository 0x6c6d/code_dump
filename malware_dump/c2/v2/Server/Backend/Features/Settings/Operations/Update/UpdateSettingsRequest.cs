using System.ComponentModel.DataAnnotations;

namespace Backend.Features.Settings.Operations.Update;

public class UpdateSettingsRequest : IRequest<Unit>
{
    public Guid Id { get; set; }
    public required int Port { get; set; }
    public required string DefaultCommand { get; set; }
    public required int BufferSizeReceive { get; set; }
}