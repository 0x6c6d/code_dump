namespace Web.Models.Entities;

public class ArticleModel
{
    [JsonPropertyName("articleId")]
    public Guid ArticleId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("itemNumber")]
    public string ItemNumber { get; set; } = string.Empty;

    [JsonPropertyName("storageBin")]
    public string StorageBin { get; set; } = string.Empty;

    [JsonPropertyName("imageUrl")]
    public string ImageUrl { get; set; } = string.Empty;

    [JsonPropertyName("shoppingUrl")]
    public string ShoppingUrl { get; set; } = string.Empty;

    [JsonPropertyName("stock")]
    public int Stock { get; set; }

    [JsonPropertyName("minStock")]
    public int MinStock { get; set; }

    [JsonPropertyName("group")]
    public GroupModel Group { get; set; } = new();

    [JsonPropertyName("operationArea")]
    public OperationAreaModel OperationArea { get; set; } = new();

    [JsonPropertyName("storagePlace")]
    public StoragePlaceModel StoragePlace { get; set; } = new();

    [JsonPropertyName("procurement")]
    public ProcurementModel Procurement { get; set; } = new();
}
