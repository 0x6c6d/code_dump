using Application.Features.WarehouseManager.Articles.Models;

namespace Application.Features.WarehouseManager.Articles.Repositories;
public class ArticleRepository : BaseRepository<Article>, IArticleRepository
{
    public ArticleRepository(DataContext dbContext) : base(dbContext) { }

    // check if group, storage place etc. is in use to prevent its deletion
    public Task<bool> FindAnyArticleWithEntityId(Func<Article, bool> predicate)
    {
        var match = _dbContext.Articles.Any(predicate);
        return Task.FromResult(match);
    }
}
