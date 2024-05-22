using Application.Features.WarehouseManager.Articles.Models;
using Application.Features.WarehouseManager.Articles.Operations.Delete;
using Application.Features.WarehouseManager.Articles.Operations.Read.All;
using Application.Features.WarehouseManager.Articles.Operations.Read.One;
using Application.Features.WarehouseManager.Categories.Operations.Read.One;

namespace Application.Features.WarehouseManager.Articles.Services;
public class ArticleService : IArticleService
{
    private readonly IMediator _mediator;

    public ArticleService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<ServiceResponse<Article>> GetArticleAsync(Guid id)
    {
        try
        {
            var result = await _mediator.Send(new GetArticleRequest { Id = id });
            if (result == null)
            {
                return new ServiceResponse<Article>
                {
                    Success = false,
                    Message = $"Keinen Artikel mit der ID {id} gefunden."
                };
            }

            var article = ArticleMapper.GetArticleFromGetArticleReturn(result);
            return new ServiceResponse<Article> { Data = article };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<Article>
            {
                Success = false,
                Message = ex.Message
            };
        }
    }

    public async Task<ServiceResponse<List<Article>>> GetArticlesAsync()
    {
        try
        {
            var result = await _mediator.Send(new GetArticlesRequest());
            if (result == null)
            {
                return new ServiceResponse<List<Article>>
                {
                    Success = false,
                    Message = "Keine Artikel gefunden."
                };
            }

            var articles = ArticleMapper.GetArticlesFromGetArticlesReturn(result);
            return new ServiceResponse<List<Article>> { Data = articles };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<List<Article>>
            {
                Success = false,
                Message = ex.Message
            };
        }
    }

    public async Task<ServiceResponse<Guid>> CreateArticleAsync(Article article)
    {
        try
        {
            var category = await _mediator.Send(new GetCategoryRequest { Id = article.CategoryId });
            if (category.Id == Guid.Empty)
            {
                return new ServiceResponse<Guid>
                {
                    Success = false,
                    Message = $"Fehler beim Abgleich der Kategorie."
                };
            }

            var createArticleRequest = ArticleMapper.CreateArticleRequestFromArticle(article);
            if (!category.Name.Equals("Ohne Kategorie", StringComparison.CurrentCultureIgnoreCase))
            {
                createArticleRequest.Stock = 1;
                createArticleRequest.MinStock = 1;
            }

            var result = await _mediator.Send(createArticleRequest);
            if (result == Guid.Empty)
            {
                return new ServiceResponse<Guid>
                {
                    Success = false,
                    Message = $"Fehler beim Erstellen des Artikels."
                };
            }

            return new ServiceResponse<Guid> { Data = result };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<Guid>
            {
                Success = false,
                Message = ex.Message
            };
        }
    }

    public async Task<ServiceResponse<bool>> UpdateArticleAsync(Article article)
    {
        try
        {
            var updateArticleRequest = ArticleMapper.UpdateArticleRequestFromArticle(article);
            await _mediator.Send(updateArticleRequest);
            return new ServiceResponse<bool> { Data = true };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<bool>
            {
                Success = false,
                Message = ex.Message
            };
        }
    }

    public async Task<ServiceResponse<bool>> DeleteArticleAsync(Guid id)
    {
        try
        {
            await _mediator.Send(new DeleteArticleRequest() { Id = id });
            return new ServiceResponse<bool> { Data = true };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<bool>
            {
                Success = false,
                Message = ex.Message
            };
        }
    }
}
