using Application.Features.WarehouseManager.Articles.Models;

namespace Application.Features.WarehouseManager.Articles.Repositories;
public interface IArticleRepository : IRepository<Article>
{
    Task<bool> FindAnyArticleWithEntityId(Func<Article, bool> predicate);
}
