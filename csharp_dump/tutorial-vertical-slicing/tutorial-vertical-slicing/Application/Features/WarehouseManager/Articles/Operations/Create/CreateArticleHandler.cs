using Application.Features.WarehouseManager.Articles.Repositories;

namespace Application.Features.WarehouseManager.Articles.Operations.Create;
public class CreateArticleHandler : IRequestHandler<CreateArticleRequest, Guid>
{
    private readonly IArticleRepository _articleRepository;

    public CreateArticleHandler(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public async Task<Guid> Handle(CreateArticleRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateArticleValidator();
        var validationResult = await validator.ValidateAsync(request);
        if (validationResult.Errors.Count > 0)
            throw new ValidationException(validationResult.Errors);

        // Mapping
        var article = ArticleMapper.CreateArticleRequestToArticle(request);
        article.CreatedDate = DateTime.Now;
        article.LastModifiedDate = DateTime.Now;
        if (article.ProcurementId == Guid.Empty)
        {
            article.ProcurementId = null;
        }
        article.Category = null;
        article.Group = null;
        article.Category = null;
        article.OperationArea = null;
        article.StoragePlace = null;
        article.Procurement = null;

        article = await _articleRepository.AddAsync(article);

        return article.Id;
    }
}
