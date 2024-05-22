
using Application.Features.WarehouseManager.Articles.Models;

namespace Application.Features.WarehouseManager.Articles.Services;

public interface IArticleService
{
    Task<ServiceResponse<Guid>> CreateArticleAsync(Article article);
    Task<ServiceResponse<bool>> DeleteArticleAsync(Guid id);
    Task<ServiceResponse<Article>> GetArticleAsync(Guid id);
    Task<ServiceResponse<List<Article>>> GetArticlesAsync();
    Task<ServiceResponse<bool>> UpdateArticleAsync(Article article);
}