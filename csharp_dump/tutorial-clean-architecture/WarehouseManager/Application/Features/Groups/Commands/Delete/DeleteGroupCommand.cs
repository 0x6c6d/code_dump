namespace Application.Features.Groups.Commands.Delete;
public class DeleteGroupCommand : IRequest<Unit>
{
    public Guid GroupId { get; set; }
}
