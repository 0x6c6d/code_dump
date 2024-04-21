namespace Application.Features.Articles.Queries.GetArticles;
public class GetArticlesQueryHandler : IRequestHandler<GetArticlesQuery, List<GetArticlesVm>>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IGroupRepository _groupRepository;
    private readonly IOperationAreaRepository _operationAreaRepository;
    private readonly IStoragePlaceRepository _storagePlaceRepository;
    private readonly IProcurementRepository _procurementRepository;

    public GetArticlesQueryHandler(IArticleRepository articleRepository, IGroupRepository groupRepository, IOperationAreaRepository operationAreaRepository, IStoragePlaceRepository storagePlaceRepository, IProcurementRepository procurementRepository)
    {
        _articleRepository = articleRepository;
        _groupRepository = groupRepository;
        _operationAreaRepository = operationAreaRepository;
        _storagePlaceRepository = storagePlaceRepository;
        _procurementRepository = procurementRepository;
    }


    public async Task<List<GetArticlesVm>> Handle(GetArticlesQuery request, CancellationToken cancellationToken)
    {
        List<GetArticlesVm> articlesVm = new();

        var articles = (await _articleRepository.GetAllAsync()).OrderBy(a => a.Name);
        if (articles.Count() <= 0)
            return new();

        // automatically populates each article object Group, OperationArea, StoragePlace & Procurement attribute inside the list of articles
        await _groupRepository.GetAllAsync();
        await _operationAreaRepository.GetAllAsync();
        await _storagePlaceRepository.GetAllAsync();
        await _procurementRepository.GetAllAsync();

        foreach (var article in articles)
        {
            var articleVm = Mappers.ArticleToGetArticlesVm(article);
            articlesVm.Add(articleVm);
        }

        return articlesVm;
    }
}
