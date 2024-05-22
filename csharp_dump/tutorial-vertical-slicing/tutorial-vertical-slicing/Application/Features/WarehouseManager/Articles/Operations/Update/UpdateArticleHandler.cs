using Application.Features.WarehouseManager.Articles.Models;
using Application.Features.WarehouseManager.Articles.Repositories;
using Microsoft.Extensions.Configuration;

namespace Application.Features.WarehouseManager.Articles.Operations.Update;
public class UpdateArticleHandler : IRequestHandler<UpdateArticleRequest, Unit>
{
    private readonly IConfiguration _configuration;
    private readonly IArticleRepository _articleRepository;

    public UpdateArticleHandler(IConfiguration configuration, IArticleRepository articleRepository)
    {
        _configuration = configuration;
        _articleRepository = articleRepository;
    }

    public async Task<Unit> Handle(UpdateArticleRequest request, CancellationToken cancellationToken)
    {
        var article = await _articleRepository.GetByIdAsync(request.Id);
        if (article == null)
            throw new NotFoundException(nameof(Article), request.Id);

        var validator = new UpdateArticleValidator();
        var validationResult = await validator.ValidateAsync(request);
        if (validationResult.Errors.Count > 0)
            throw new ValidationException(validationResult.Errors);

        // Mapping
        ArticleMapper.UpdateArticleRequestToArticle(request, article);
        article.LastModifiedDate = DateTime.Now;
        if (article.ProcurementId == Guid.Empty)
        {
            article.ProcurementId = null;
        }

        await _articleRepository.UpdateAsync(article);

        return Unit.Value;
    }
}
