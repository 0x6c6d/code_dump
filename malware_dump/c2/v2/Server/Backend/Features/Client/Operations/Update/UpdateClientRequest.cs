namespace Backend.Features.Client.Operations.Update;

public class UpdateClientRequest : IRequest<Unit>
{
    public required Guid Id { get; set; }
    public required string Ip { get; set; }
    public required int Port { get; set; }
    public required string Command { get; set; }
}