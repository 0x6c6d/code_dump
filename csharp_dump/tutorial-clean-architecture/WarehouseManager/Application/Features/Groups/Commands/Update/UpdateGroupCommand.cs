using System.Text.Json.Serialization;

namespace Application.Features.Groups.Commands.Update;
public class UpdateGroupCommand : IRequest<Unit>
{
    [JsonPropertyName("id")]
    public Guid GroupId { get; set; }
    public string Name { get; set; } = string.Empty;
}
