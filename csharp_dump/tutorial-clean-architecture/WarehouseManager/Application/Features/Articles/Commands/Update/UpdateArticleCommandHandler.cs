using Application.Contracts.Infrastructure;

namespace Application.Features.Articles.Commands.Update;
public class UpdateArticleCommandHandler : IRequestHandler<UpdateArticleCommand, Unit>
{
    private readonly IConfiguration _configuration;
    private readonly IEmailService _emailService;
    private readonly IArticleRepository _articleRepository;

    public UpdateArticleCommandHandler(IConfiguration configuration, IEmailService emailService, IArticleRepository articleRepository)
    {
        _configuration = configuration;
        _emailService = emailService;
        _articleRepository = articleRepository;
    }

    public async Task<Unit> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
    {
        var article = await _articleRepository.GetByIdAsync(request.ArticleId);
        if (article == null)
            throw new NotFoundException(nameof(Article), request.ArticleId);

        // create ticket if min stock is reached
        // only send mail if min stock is reached: not on every update where stock is below minstock
        // only send mail if minstock is reached from above: 11 -> 10 & not 9 -> 10
        if (request.Stock == request.MinStock && request.Stock < article.Stock)
        {
            var email = new Email()
            {
                Subject = $"Lagerverwaltung: Bestand des Artikels '{article.Name}' ist niedrig",
                Body =  $"Name: {article.Name}\nArtikelnummer: {article.ItemNumber}\n\nDer Mindestbestand des Artikels ist erreicht worden. Bitte nachbestellen."
            };
            _emailService.SendEmail(email);
        }

        var validator = new UpdateArticleCommandValidator(_articleRepository);
        var validationResult = await validator.ValidateAsync(request);
        if (validationResult.Errors.Count > 0)
            throw new Exceptions.ValidationException(validationResult);

        Mappers.UpdateArticleCommandToArticle(request, article);
        article.LastModifiedDate = DateTime.Now;
        await _articleRepository.UpdateAsync(article);

        return Unit.Value;
    }
}
