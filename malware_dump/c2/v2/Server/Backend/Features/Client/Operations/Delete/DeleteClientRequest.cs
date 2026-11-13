namespace Backend.Features.Client.Operations.Delete;

public class DeleteClientRequest : IRequest<Unit>
{
    public Guid Id { get; set; }
}