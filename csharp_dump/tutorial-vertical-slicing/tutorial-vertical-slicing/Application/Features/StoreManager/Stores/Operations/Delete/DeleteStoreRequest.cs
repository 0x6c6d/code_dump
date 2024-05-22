namespace Application.Features.StoreManager.Stores.Operations.Delete;
public class DeleteStoreRequest : IRequest<Unit>
{
    public string StoreId { get; set; } = string.Empty;
}
