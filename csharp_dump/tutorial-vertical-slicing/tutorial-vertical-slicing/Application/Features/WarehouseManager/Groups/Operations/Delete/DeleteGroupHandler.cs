using Application.Features.WarehouseManager.Articles.Repositories;
using Application.Features.WarehouseManager.Groups.Models;
using Application.Features.WarehouseManager.Groups.Repositories;

namespace Application.Features.WarehouseManager.Groups.Operations.Delete;
public class DeleteGroupHandler : IRequestHandler<DeleteGroupRequest, Unit>
{
    private readonly IGroupRepository _groupRepository;
    private readonly IArticleRepository _articleRepository;

    public DeleteGroupHandler(IGroupRepository groupRepository, IArticleRepository articleRepository)
    {
        _groupRepository = groupRepository;
        _articleRepository = articleRepository;
    }

    public async Task<Unit> Handle(DeleteGroupRequest request, CancellationToken cancellationToken)
    {
        var group = await _groupRepository.GetByIdAsync(request.Id);
        if (group == null)
            throw new NotFoundException(nameof(Group), request.Id);

        var match = await _articleRepository.FindAnyArticleWithEntityId(a => a.GroupId == request.Id);
        if (match)
            throw new InUseException(nameof(Group), request.Id);

        await _groupRepository.DeleteAsync(group);


        return Unit.Value;
    }
}
