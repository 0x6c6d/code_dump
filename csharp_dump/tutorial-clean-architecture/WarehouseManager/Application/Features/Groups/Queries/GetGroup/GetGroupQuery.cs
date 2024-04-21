namespace Application.Features.Groups.Queries.GetGroup;
public class GetGroupQuery : IRequest<GetGroupVm>
{
    public Guid GroupId { get; set; }
}
