
namespace Web.Services.BL;

public interface IArticleService
{
    Task<DataAccessResponse<Guid>> CreateArticle(ArticleModel article);
    Task<DataAccessResponse<bool>> DeleteArticle(Guid id);
    Task<DataAccessResponse<ArticleModel>> GetArticle(Guid id);
    Task<DataAccessResponse<List<ArticleModel>>> GetArticles();
    Task<DataAccessResponse<bool>> UpdateArticle(ArticleModel article);
}