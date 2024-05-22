using Application.Features.WarehouseManager.Categories.Models;
using Application.Features.WarehouseManager.Categories.Repositories;

namespace Application.Features.WarehouseManager.Categories.Operations.Read.One;
public class GetCategoryHandler : IRequestHandler<GetCategoryRequest, GetCategoryReturn>
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<GetCategoryReturn> Handle(GetCategoryRequest request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.Id);

        if (category == null)
            throw new NotFoundException(nameof(Category), request.Id);

        var categoryVm = CategoryMapper.CategoryToGetCategoryReturn(category);

        return categoryVm;
    }
}
