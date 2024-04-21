namespace Application.Features.Articles.Queries.GetArticle;
public class GetArticleQueryHandler : IRequestHandler<GetArticleQuery, GetArticleVm>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IGroupRepository _groupRepository;
    private readonly IOperationAreaRepository _operationAreaRepository;
    private readonly IStoragePlaceRepository _storagePlaceRepository;
    private readonly IProcurementRepository _procurementRepository;

    public GetArticleQueryHandler(IArticleRepository articleRepository, IGroupRepository groupRepository, IOperationAreaRepository operationAreaRepository, IStoragePlaceRepository storagePlaceRepository, IProcurementRepository procurementRepository)
    {
        _articleRepository = articleRepository;
        _groupRepository = groupRepository;
        _operationAreaRepository = operationAreaRepository;
        _storagePlaceRepository = storagePlaceRepository;
        _procurementRepository = procurementRepository;
    }

    public async Task<GetArticleVm> Handle(GetArticleQuery request, CancellationToken cancellationToken)
    {
        var article = await _articleRepository.GetByIdAsync(request.ArticleId);

        if (article == null)
            throw new NotFoundException(nameof(Article), request.ArticleId);

        var articleVm = Mappers.ArticleToGetArticleVm(article);

        articleVm.Group = await _groupRepository.GetByIdAsync(article.GroupId) ?? new();
        articleVm.OperationArea = await _operationAreaRepository.GetByIdAsync(article.OperationAreaId) ?? new();
        articleVm.StoragePlace = await _storagePlaceRepository.GetByIdAsync(article.StoragePlaceId) ?? new();
        articleVm.Procurement = await _procurementRepository.GetByIdAsync(article.ProcurementId) ?? new();

        return articleVm;
    }
}

