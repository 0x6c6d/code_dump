namespace Application.Features.Groups.Commands.Delete;
public class DeleteGroupCommandHandler : IRequestHandler<DeleteGroupCommand, Unit>
{
    private readonly IGroupRepository _groupRepository;
    private readonly IArticleRepository _articleRepository;

    public DeleteGroupCommandHandler(IGroupRepository groupRepository, IArticleRepository articleRepository)
    {
        _groupRepository = groupRepository;
        _articleRepository = articleRepository;
    }

    public async Task<Unit> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await _groupRepository.GetByIdAsync(request.GroupId);
        if (group == null)
            throw new NotFoundException(nameof(Group), request.GroupId);

        var match = await _articleRepository.FindAnyArticleWithEntityId(a => a.GroupId == request.GroupId);
        if (match)
            throw new InUseException(nameof(Group), request.GroupId);

        await _groupRepository.DeleteAsync(group);


        return Unit.Value;
    }
}
