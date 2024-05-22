namespace Application.Features.StoreManager.Stores.Operations.Update;
public class UpdateStoreRequest : IRequest<Unit>
{
    public string StoreId { get; set; } = string.Empty;
    public string? Description { get; set; }
}
