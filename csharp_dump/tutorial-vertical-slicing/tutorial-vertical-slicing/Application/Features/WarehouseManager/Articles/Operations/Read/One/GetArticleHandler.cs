using Application.Features.WarehouseManager.Articles.Models;
using Application.Features.WarehouseManager.Articles.Repositories;
using Application.Features.WarehouseManager.Categories.Repositories;
using Application.Features.WarehouseManager.Groups.Repositories;
using Application.Features.WarehouseManager.OperationAreas.Repositories;
using Application.Features.WarehouseManager.Procurements.Repositories;
using Application.Features.WarehouseManager.StoragePlaces.Repositories;

namespace Application.Features.WarehouseManager.Articles.Operations.Read.One;
public class GetArticleHandler : IRequestHandler<GetArticleRequest, GetArticleReturn>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IGroupRepository _groupRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IOperationAreaRepository _operationAreaRepository;
    private readonly IStoragePlaceRepository _storagePlaceRepository;
    private readonly IProcurementRepository _procurementRepository;

    public GetArticleHandler(IArticleRepository articleRepository, IGroupRepository groupRepository, ICategoryRepository categoryRepository, IOperationAreaRepository operationAreaRepository, IStoragePlaceRepository storagePlaceRepository, IProcurementRepository procurementRepository)
    {
        _articleRepository = articleRepository;
        _groupRepository = groupRepository;
        _categoryRepository = categoryRepository;
        _operationAreaRepository = operationAreaRepository;
        _storagePlaceRepository = storagePlaceRepository;
        _procurementRepository = procurementRepository;
    }

    public async Task<GetArticleReturn> Handle(GetArticleRequest request, CancellationToken cancellationToken)
    {
        var article = await _articleRepository.GetByIdAsync(request.Id);

        if (article == null)
            throw new NotFoundException(nameof(Article), request.Id);

        var articleVm = ArticleMapper.ArticleToGetArticleReturn(article);

        articleVm.Group = await _groupRepository.GetByIdAsync(article.GroupId) ?? new();
        articleVm.Category = await _categoryRepository.GetByIdAsync(article.CategoryId) ?? new();
        articleVm.OperationArea = await _operationAreaRepository.GetByIdAsync(article.OperationAreaId) ?? new();
        articleVm.StoragePlace = await _storagePlaceRepository.GetByIdAsync(article.StoragePlaceId) ?? new();
        articleVm.Procurement = await _procurementRepository.GetByIdAsync(article.ProcurementId ?? Guid.Empty) ?? new();

        return articleVm;
    }
}

