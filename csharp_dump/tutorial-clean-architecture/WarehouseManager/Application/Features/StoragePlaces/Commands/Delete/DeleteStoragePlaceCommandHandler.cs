namespace Application.Features.StoragePlaces.Commands.Delete;
public class DeleteStoragePlaceCommandHandler : IRequestHandler<DeleteStoragePlaceCommand, Unit>
{
    private readonly IStoragePlaceRepository _storagePlaceRepository;
    private readonly IArticleRepository _articleRepository;

    public DeleteStoragePlaceCommandHandler(IStoragePlaceRepository storagePlaceRepository, IArticleRepository articleRepository)
    {
        _storagePlaceRepository = storagePlaceRepository;
        _articleRepository = articleRepository;
    }

    public async Task<Unit> Handle(DeleteStoragePlaceCommand request, CancellationToken cancellationToken)
    {
        var storagePlace = await _storagePlaceRepository.GetByIdAsync(request.StoragePlaceId);
        if (storagePlace == null)
            throw new NotFoundException(nameof(StoragePlace), request.StoragePlaceId);

        var match = await _articleRepository.FindAnyArticleWithEntityId(a => a.StoragePlaceId == request.StoragePlaceId);
        if (match)
            throw new InUseException(nameof(Group), request.StoragePlaceId);

        await _storagePlaceRepository.DeleteAsync(storagePlace);

        return Unit.Value;
    }
}
