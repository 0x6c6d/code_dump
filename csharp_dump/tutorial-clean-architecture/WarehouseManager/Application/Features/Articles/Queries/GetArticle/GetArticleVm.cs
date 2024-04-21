namespace Application.Features.Articles.Queries.GetArticle;
// ArticleVm is a view model that provides details of a single article
public class GetArticleVm
{
    public Guid ArticleId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ItemNumber { get; set; } = string.Empty;
    public string StorageBin { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string ShoppingUrl { get; set; } = string.Empty;
    public int Stock { get; set; }
    public int MinStock { get; set; }
    public Group Group { get; set; } = new();
    public OperationArea OperationArea { get; set; } = new();
    public StoragePlace StoragePlace { get; set; } = new();
    public Procurement Procurement { get; set; } = new();
}