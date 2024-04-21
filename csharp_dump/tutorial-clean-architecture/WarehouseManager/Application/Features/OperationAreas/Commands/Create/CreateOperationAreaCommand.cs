namespace Application.Features.OperationAreas.Commands.Create;
public class CreateOperationAreaCommand : IRequest<Guid>
{
    public string Name { get; set; } = string.Empty;
}
