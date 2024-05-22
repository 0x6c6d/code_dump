namespace Application.Features.WarehouseManager.Groups.Operations.Create;
public class CreateGroupRequest : IRequest<Guid>
{
    public string Name { get; set; } = string.Empty;
}
