namespace Web.Services.BL;

public class ArticleService : IArticleService
{
    private readonly IDataAccessArticleService _dataAccessArticleService;

    public ArticleService(IDataAccessArticleService dataAccessArticleService)
    {
        _dataAccessArticleService = dataAccessArticleService;
    }

    public async Task<DataAccessResponse<ArticleModel>> GetArticle(Guid id)
    {
        if (id == Guid.Empty)
            return new DataAccessResponse<ArticleModel>
            {
                Success = false,
                Message = "ArticleId is empty."
            };

        var result = await _dataAccessArticleService.GetArticleAsync(id);
        return result;
    }

    public async Task<DataAccessResponse<List<ArticleModel>>> GetArticles()
    {
        var result = await _dataAccessArticleService.GetArticlesAsync();
        return result;
    }

    public async Task<DataAccessResponse<Guid>> CreateArticle(ArticleModel article)
    {
        var result = await _dataAccessArticleService.CreateArticleyAsync(article);
        return result;
    }

    public async Task<DataAccessResponse<bool>> UpdateArticle(ArticleModel article)
    {
        var result = await _dataAccessArticleService.UpdateArticleAsync(article);
        return result;
    }

    public async Task<DataAccessResponse<bool>> DeleteArticle(Guid id)
    {
        var result = await _dataAccessArticleService.DeleteArticleAsync(id);
        return result;
    }
}
