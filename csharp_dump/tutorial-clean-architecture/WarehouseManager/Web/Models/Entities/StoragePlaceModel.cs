namespace Web.Models.Entities;

public class StoragePlaceModel
{
    [JsonPropertyName("storagePlaceId")]
    public Guid StoragePlaceId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
}
