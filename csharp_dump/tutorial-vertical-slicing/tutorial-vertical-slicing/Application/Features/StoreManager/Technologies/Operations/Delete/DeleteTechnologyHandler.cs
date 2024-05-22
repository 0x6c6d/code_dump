using Application.Features.StoreManager.Technologies.Models;
using Application.Features.StoreManager.Technologies.Repositories;

namespace Application.Features.StoreManager.Technologies.Operations.Delete;
public class DeleteTechnologyHandler : IRequestHandler<DeleteTechnologyRequest, Unit>
{
    private readonly ITechnologyRepository _technologyService;

    public DeleteTechnologyHandler(ITechnologyRepository technologyService)
    {
        _technologyService = technologyService;
    }

    public async Task<Unit> Handle(DeleteTechnologyRequest request, CancellationToken cancellationToken)
    {
        var technology = await _technologyService.GetByStoreIdAsync(request.StoreId);
        if (technology == null)
            throw new NotFoundException(nameof(Technology), request.StoreId);

        await _technologyService.DeleteAsync(technology);

        return Unit.Value;
    }
}
