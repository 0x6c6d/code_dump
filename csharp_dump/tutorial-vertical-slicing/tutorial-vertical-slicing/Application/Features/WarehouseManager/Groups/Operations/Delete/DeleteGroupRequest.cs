namespace Application.Features.WarehouseManager.Groups.Operations.Delete;
public class DeleteGroupRequest : IRequest<Unit>
{
    public Guid Id { get; set; }
}
