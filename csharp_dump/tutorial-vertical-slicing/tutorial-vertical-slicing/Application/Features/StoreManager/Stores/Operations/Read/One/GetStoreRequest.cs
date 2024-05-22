namespace Application.Features.StoreManager.Stores.Operations.Read.One;
public class GetStoreRequest : IRequest<GetStoreReturn>
{
    public string StoreId { get; set; } = string.Empty;
}
