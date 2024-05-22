namespace Application.Features.WarehouseManager.Groups.Operations.Read.One;
public class GetGroupRequest : IRequest<GetGroupReturn>
{
    public Guid Id { get; set; }
}
