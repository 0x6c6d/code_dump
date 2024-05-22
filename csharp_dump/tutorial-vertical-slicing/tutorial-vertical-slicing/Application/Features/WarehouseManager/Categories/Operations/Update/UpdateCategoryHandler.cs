using Application.Features.WarehouseManager.Categories.Models;
using Application.Features.WarehouseManager.Categories.Repositories;

namespace Application.Features.WarehouseManager.Categories.Operations.Update;
public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryRequest, Unit>
{
    private readonly ICategoryRepository _categoryRepository;

    public UpdateCategoryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Unit> Handle(UpdateCategoryRequest request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.Id);
        if (category == null)
            throw new NotFoundException(nameof(Category), request.Id);

        var validator = new UpdateCategoryValidator(_categoryRepository);
        var validationResult = await validator.ValidateAsync(request);
        if (validationResult.Errors.Count > 0)
            throw new ValidationException(validationResult.Errors);

        CategoryMapper.UpdateCategoryRequestToCategory(request, category);
        category.LastModifiedDate = DateTime.Now;
        await _categoryRepository.UpdateAsync(category);

        return Unit.Value;
    }
}