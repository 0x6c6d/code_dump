namespace Application.Contracts.Persistence;
public interface IArticleRepository : IRepository<Article>
{
    Task<bool> IsArticleNameAndItemNumberUnique(string name, string itemNumber);
    Task<bool> FindAnyArticleWithEntityId(Func<Article, bool> predicate);
}
