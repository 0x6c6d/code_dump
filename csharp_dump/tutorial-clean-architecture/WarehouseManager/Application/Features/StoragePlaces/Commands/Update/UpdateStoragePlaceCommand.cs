using System.Text.Json.Serialization;

namespace Application.Features.StoragePlaces.Commands.Update;
public class UpdateStoragePlaceCommand : IRequest<Unit>
{
    [JsonPropertyName("id")]
    public Guid StoragePlaceId { get; set; }
    public string Name { get; set; } = string.Empty;
}
