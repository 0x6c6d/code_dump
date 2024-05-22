namespace Application.Features.WarehouseManager.Groups.Operations.Update;
public class UpdateGroupRequest : IRequest<Unit>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
