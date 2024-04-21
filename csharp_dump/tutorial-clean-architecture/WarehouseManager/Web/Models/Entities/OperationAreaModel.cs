namespace Web.Models.Entities;

public class OperationAreaModel
{
    [JsonPropertyName("operationAreaId")]
    public Guid OperationAreaId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
}
