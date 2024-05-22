using Application.Features.WarehouseManager.Articles.Models;
using Application.Features.WarehouseManager.Articles.Repositories;

namespace Application.Features.WarehouseManager.Articles.Operations.Delete;
public class DeleteArticleHandler : IRequestHandler<DeleteArticleRequest, Unit>
{
    private readonly IArticleRepository _articleRepository;

    public DeleteArticleHandler(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public async Task<Unit> Handle(DeleteArticleRequest request, CancellationToken cancellationToken)
    {
        var article = await _articleRepository.GetByIdAsync(request.Id);
        if (article == null)
            throw new NotFoundException(nameof(Article), request.Id);

        await _articleRepository.DeleteAsync(article);

        return Unit.Value;
    }
}
