namespace Backend.Features.Command.Operations.Delete.ById;

public class DeleteCommandByIdRequest : IRequest<Unit>
{
    public Guid Id { get; set; }
}