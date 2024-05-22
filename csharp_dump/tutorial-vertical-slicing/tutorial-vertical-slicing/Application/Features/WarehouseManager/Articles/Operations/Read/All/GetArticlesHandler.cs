using Application.Features.WarehouseManager.Articles;
using Application.Features.WarehouseManager.Articles.Repositories;
using Application.Features.WarehouseManager.Categories.Repositories;
using Application.Features.WarehouseManager.Groups.Repositories;
using Application.Features.WarehouseManager.OperationAreas.Repositories;
using Application.Features.WarehouseManager.Procurements.Repositories;
using Application.Features.WarehouseManager.StoragePlaces.Repositories;

namespace Application.Features.WarehouseManager.Articles.Operations.Read.All;
public class GetArticlesHandler : IRequestHandler<GetArticlesRequest, List<GetArticlesReturn>>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IGroupRepository _groupRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IOperationAreaRepository _operationAreaRepository;
    private readonly IStoragePlaceRepository _storagePlaceRepository;
    private readonly IProcurementRepository _procurementRepository;

    public GetArticlesHandler(IArticleRepository articleRepository, IGroupRepository groupRepository, ICategoryRepository categoryRepository, IOperationAreaRepository operationAreaRepository, IStoragePlaceRepository storagePlaceRepository, IProcurementRepository procurementRepository)
    {
        _articleRepository = articleRepository;
        _groupRepository = groupRepository;
        _categoryRepository = categoryRepository;
        _operationAreaRepository = operationAreaRepository;
        _storagePlaceRepository = storagePlaceRepository;
        _procurementRepository = procurementRepository;
    }


    public async Task<List<GetArticlesReturn>> Handle(GetArticlesRequest request, CancellationToken cancellationToken)
    {
        List<GetArticlesReturn> articlesVm = new();

        var articles = (await _articleRepository.GetAllAsync()).OrderBy(a => a.Name);
        if (articles.Count() <= 0)
            return new();

        // automatically populates each article object Group, OperationArea, StoragePlace & Procurement attribute inside the list of articles
        await _groupRepository.GetAllAsync();
        await _categoryRepository.GetAllAsync();
        await _operationAreaRepository.GetAllAsync();
        await _storagePlaceRepository.GetAllAsync();
        await _procurementRepository.GetAllAsync();

        foreach (var article in articles)
        {
            var articleVm = ArticleMapper.ArticleToGetArticlesReturn(article);
            articlesVm.Add(articleVm);
        }

        return articlesVm;
    }
}
