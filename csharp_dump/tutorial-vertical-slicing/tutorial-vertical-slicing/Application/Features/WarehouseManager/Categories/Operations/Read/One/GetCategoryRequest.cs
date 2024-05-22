namespace Application.Features.WarehouseManager.Categories.Operations.Read.One;
public class GetCategoryRequest : IRequest<GetCategoryReturn>
{
    public Guid Id { get; set; }
}
