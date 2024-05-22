using Application.Features.WarehouseManager.Articles.Repositories;
using Application.Features.WarehouseManager.StoragePlaces.Models;
using Application.Features.WarehouseManager.StoragePlaces.Repositories;

namespace Application.Features.WarehouseManager.StoragePlaces.Operations.Delete;
public class DeleteStoragePlaceHandler : IRequestHandler<DeleteStoragePlaceRequest, Unit>
{
    private readonly IStoragePlaceRepository _storagePlaceRepository;
    private readonly IArticleRepository _articleRepository;

    public DeleteStoragePlaceHandler(IStoragePlaceRepository storagePlaceRepository, IArticleRepository articleRepository)
    {
        _storagePlaceRepository = storagePlaceRepository;
        _articleRepository = articleRepository;
    }

    public async Task<Unit> Handle(DeleteStoragePlaceRequest request, CancellationToken cancellationToken)
    {
        var storagePlace = await _storagePlaceRepository.GetByIdAsync(request.Id);
        if (storagePlace == null)
            throw new NotFoundException(nameof(StoragePlace), request.Id);

        var match = await _articleRepository.FindAnyArticleWithEntityId(a => a.StoragePlaceId == request.Id);
        if (match)
            throw new InUseException(nameof(StoragePlace), request.Id);

        await _storagePlaceRepository.DeleteAsync(storagePlace);

        return Unit.Value;
    }
}
