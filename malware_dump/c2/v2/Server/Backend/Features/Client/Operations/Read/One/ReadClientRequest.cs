namespace Backend.Features.Client.Operations.Read.One;

public class ReadClientRequest : IRequest<ReadClientReturn>
{
    public required string Ip { get; set; }
}