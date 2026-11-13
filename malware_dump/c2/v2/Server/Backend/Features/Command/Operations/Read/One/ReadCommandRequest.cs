namespace Backend.Features.Command.Operations.Read.One;

public class ReadCommandRequest : IRequest<ReadCommandReturn>
{
    public Guid Id { get; set; }
}