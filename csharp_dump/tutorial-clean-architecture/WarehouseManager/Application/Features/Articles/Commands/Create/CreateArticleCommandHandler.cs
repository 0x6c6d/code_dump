namespace Application.Features.Articles.Commands.Create;
public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, Guid>
{
    private readonly IArticleRepository _articleRepository;

    public CreateArticleCommandHandler(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public async Task<Guid> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateArticleCommandValidator(_articleRepository);
        var validationResult = await validator.ValidateAsync(request);
        if (validationResult.Errors.Count > 0)
            throw new Exceptions.ValidationException(validationResult);

        var article = Mappers.CreateArticleCommandToArticle(request);
        article.CreatedDate = DateTime.Now;
        article.LastModifiedDate = DateTime.Now;
        article = await _articleRepository.AddAsync(article);

        return article.ArticleId;
    }
}
