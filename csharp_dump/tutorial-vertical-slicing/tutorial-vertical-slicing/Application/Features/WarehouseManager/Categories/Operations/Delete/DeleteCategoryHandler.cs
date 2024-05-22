using Application.Features.WarehouseManager.Articles.Repositories;
using Application.Features.WarehouseManager.Categories.Models;
using Application.Features.WarehouseManager.Categories.Repositories;

namespace Application.Features.WarehouseManager.Categories.Operations.Delete;
public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryRequest, Unit>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IArticleRepository _articleRepository;

    public DeleteCategoryHandler(ICategoryRepository categoryRepository, IArticleRepository articleRepository)
    {
        _categoryRepository = categoryRepository;
        _articleRepository = articleRepository;
    }

    public async Task<Unit> Handle(DeleteCategoryRequest request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.Id);
        if (category == null)
            throw new NotFoundException(nameof(Category), request.Id);

        var match = await _articleRepository.FindAnyArticleWithEntityId(a => a.CategoryId == request.Id);
        if (match)
            throw new InUseException(nameof(Category), request.Id);

        await _categoryRepository.DeleteAsync(category);

        return Unit.Value;
    }
}
