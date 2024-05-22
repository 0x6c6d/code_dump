namespace Application.Features.WarehouseManager.Categories.Operations.Delete;
public class DeleteCategoryRequest : IRequest<Unit>
{
    public Guid Id { get; set; }
}
