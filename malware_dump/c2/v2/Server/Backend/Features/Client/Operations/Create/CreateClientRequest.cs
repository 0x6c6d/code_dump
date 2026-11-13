namespace Backend.Features.Client.Operations.Create;

public class CreateClientRequest : IRequest<Guid>
{
    public required string Ip { get; set; }
    public required int Port { get; set; }
    public required string Command { get; set; }
}