
namespace Web.Services.DA;

public interface IDataAccessArticleService
{
    Task<DataAccessResponse<Guid>> CreateArticleyAsync(ArticleModel article);
    Task<DataAccessResponse<bool>> DeleteArticleAsync(Guid id);
    Task<DataAccessResponse<ArticleModel>> GetArticleAsync(Guid id);
    Task<DataAccessResponse<List<ArticleModel>>> GetArticlesAsync();
    Task<DataAccessResponse<bool>> UpdateArticleAsync(ArticleModel article);
}