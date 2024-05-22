using Application.Features.WarehouseManager.Categories.Repositories;

namespace Application.Features.WarehouseManager.Categories.Operations.Read.All;
public class GetCategoriesHandler : IRequestHandler<GetCategoriesRequest, List<GetCategoriesReturn>>
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoriesHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<List<GetCategoriesReturn>> Handle(GetCategoriesRequest request, CancellationToken cancellationToken)
    {
        var categories = (await _categoryRepository.GetAllAsync()).OrderBy(u => u.Name);

        var categoriesVm = CategoryMapper.CategoriesToGetCategoriesReturn(categories);

        return categoriesVm;
    }
}
