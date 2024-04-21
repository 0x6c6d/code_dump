using System.Text.Json.Serialization;

namespace Application.Features.OperationAreas.Commands.Update;
public class UpdateOperationAreaCommand : IRequest<Unit>
{
    [JsonPropertyName("id")]
    public Guid OperationAreaId { get; set; }
    public string Name { get; set; } = string.Empty;
}
