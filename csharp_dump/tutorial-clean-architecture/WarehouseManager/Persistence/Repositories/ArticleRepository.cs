namespace Persistence.Repositories;
public class ArticleRepository : BaseRepository<Article>, IArticleRepository
{
    public ArticleRepository(DataContext dbContext) : base(dbContext)
    {
    }

    public Task<bool> IsArticleNameAndItemNumberUnique(string name, string itemNumber)
    {
        var match = _dbContext.Articles.Any(a => a.Name.Equals(name) && a.ItemNumber.Equals(itemNumber));
        return Task.FromResult(match);
    }

    // check if group, storage place etc. is in use to provent its deletion
    public Task<bool> FindAnyArticleWithEntityId(Func<Article, bool> predicate)
    {
        var match = _dbContext.Articles.Any(predicate);
        return Task.FromResult(match);
    }
}
