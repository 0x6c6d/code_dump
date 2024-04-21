namespace Application.Features.Groups.Commands.Create;
public class CreateGroupCommand : IRequest<Guid>
{
    public string Name { get; set; } = string.Empty;
}
