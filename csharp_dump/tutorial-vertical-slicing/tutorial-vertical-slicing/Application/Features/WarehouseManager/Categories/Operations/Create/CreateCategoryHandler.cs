using Application.Features.WarehouseManager.Categories.Repositories;

namespace Application.Features.WarehouseManager.Categories.Operations.Create;
public class CreateCategoryHandler : IRequestHandler<CreateCategoryRequest, Guid>
{
    private readonly ICategoryRepository _categoryRepository;

    public CreateCategoryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Guid> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateCategoryValidator(_categoryRepository);
        var validationResult = await validator.ValidateAsync(request);
        if (validationResult.Errors.Count > 0)
            throw new ValidationException(validationResult.Errors);

        var category = CategoryMapper.CreateCategoryRequestToCategory(request);
        category.CreatedDate = DateTime.Now;
        category.LastModifiedDate = DateTime.Now;
        category = await _categoryRepository.AddAsync(category);

        return category.Id;
    }
}
