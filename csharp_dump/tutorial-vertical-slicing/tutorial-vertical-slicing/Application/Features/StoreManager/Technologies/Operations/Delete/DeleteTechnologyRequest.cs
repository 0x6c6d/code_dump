namespace Application.Features.StoreManager.Technologies.Operations.Delete;
public class DeleteTechnologyRequest : IRequest<Unit>
{
    public string StoreId { get; set; } = string.Empty;
}
