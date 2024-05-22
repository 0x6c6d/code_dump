namespace Application.Features.StoreManager.Technologies.Operations.Read.Search;
public class SearchTechnologyRequest : IRequest<List<string>>
{
    public string SearchInput { get; set; } = string.Empty;
}
