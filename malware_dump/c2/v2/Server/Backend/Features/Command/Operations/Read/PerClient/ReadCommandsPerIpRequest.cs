namespace Backend.Features.Command.Operations.Read.PerClient;

public class ReadCommandsPerIpRequest : IRequest<List<ReadCommandsByIpReturn>>
{
    public required string Ip { get; set; }
}