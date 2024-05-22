namespace Application.Features.StoreManager.Stores.Operations.Create;
public class CreateStoreRequest : IRequest<string>
{
    public string StoreId { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}