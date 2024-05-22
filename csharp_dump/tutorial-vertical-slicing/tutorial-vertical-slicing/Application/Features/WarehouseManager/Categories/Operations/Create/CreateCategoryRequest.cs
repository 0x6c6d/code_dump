namespace Application.Features.WarehouseManager.Categories.Operations.Create;
public class CreateCategoryRequest : IRequest<Guid>
{
    public string Name { get; set; } = string.Empty;
}
