namespace Backend.Features.Command.Operations.Delete.ByIp;

public class DeleteCommandsByIpRequest : IRequest<Unit>
{
    public required string Ip { get; set; }
}