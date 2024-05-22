namespace Application.Features.WarehouseManager.Categories.Operations.Update;
public class UpdateCategoryRequest : IRequest<Unit>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
