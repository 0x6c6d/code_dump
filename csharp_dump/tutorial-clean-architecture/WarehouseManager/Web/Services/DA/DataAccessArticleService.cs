using System.Text;

namespace Web.Services.DA;

public class DataAccessArticleService : IDataAccessArticleService
{
    private readonly IHttpClientFactory _httpClient;
    private readonly IConfiguration _configuration;

    public DataAccessArticleService(IHttpClientFactory httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<DataAccessResponse<List<ArticleModel>>> GetArticlesAsync()
    {
        var client = _httpClient.CreateClient();
        var request = new HttpRequestMessage(HttpMethod.Get, $"{_configuration["Endpoints:API"]}/Article");

        var response = await client.SendAsync(request);
        var responseText = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        if (response.IsSuccessStatusCode)
        {
            using var responseStream = await response.Content.ReadAsStreamAsync();
            var articles = await JsonSerializer.DeserializeAsync<List<ArticleModel>>(responseStream);

            if (articles == null)
            {
                return ExceptionConverter.ConvertDataAccessExceptions<List<ArticleModel>>(new DataAccessException("The response was null which was not expected.", (int)response.StatusCode, responseText));
            }

            return new DataAccessResponse<List<ArticleModel>> { Data = articles };
        }
        else
        {
            return ExceptionConverter.ConvertDataAccessExceptions<List<ArticleModel>>(new DataAccessException("The request failed.", (int)response.StatusCode, responseText));
        }
    }

    public async Task<DataAccessResponse<ArticleModel>> GetArticleAsync(Guid id)
    {
        var client = _httpClient.CreateClient();
        var request = new HttpRequestMessage(HttpMethod.Get, $"{_configuration["Endpoints:API"]}/Article/{id}");

        var response = await client.SendAsync(request);
        var responseText = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        if (response.IsSuccessStatusCode)
        {
            using var responseStream = await response.Content.ReadAsStreamAsync();
            var article = await JsonSerializer.DeserializeAsync<ArticleModel>(responseStream);

            if (article == null)
            {
                return ExceptionConverter.ConvertDataAccessExceptions<ArticleModel>(new DataAccessException("The response was null which was not expected.", (int)response.StatusCode, responseText));
            }

            return new DataAccessResponse<ArticleModel> { Data = article };
        }
        else
        {
            return ExceptionConverter.ConvertDataAccessExceptions<ArticleModel>(new DataAccessException("The request failed.", (int)response.StatusCode, responseText));
        }
    }

    public async Task<DataAccessResponse<Guid>> CreateArticleyAsync(ArticleModel article)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, $"{_configuration["Endpoints:API"]}/Article");
        var client = _httpClient.CreateClient();

        var jsonPayload = JsonSerializer.Serialize(new
        {
            articleId = article.ArticleId,
            name = article.Name,
            itemNumber = article.ItemNumber,
            storageBin = article.StorageBin,
            groupId = article.Group.GroupId,
            operationAreaId = article.OperationArea.OperationAreaId,
            storagePlaceId = article.StoragePlace.StoragePlaceId,
            procurementId = article.Procurement.ProcurementId,
            stock = article.Stock,
            minStock = article.MinStock,
            imageUrl = article.ImageUrl,
            shoppingUrl = article.ShoppingUrl,
        });
        request.Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

        var response = await client.SendAsync(request);
        var responseText = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            var guid = JsonSerializer.Deserialize<Guid>(responseText);
            if (guid == Guid.Empty)
            {
                return ExceptionConverter.ConvertDataAccessExceptions<Guid>(new DataAccessException("The response was null which was not expected.", (int)response.StatusCode, responseText));
            }

            return new DataAccessResponse<Guid> { Data = guid };
        }
        else
        {
            return ExceptionConverter.ConvertDataAccessExceptions<Guid>(new DataAccessException("The request failed.", (int)response.StatusCode, responseText));
        }
    }

    public async Task<DataAccessResponse<bool>> UpdateArticleAsync(ArticleModel article)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, $"{_configuration["Endpoints:API"]}/Article");
        var client = _httpClient.CreateClient();

        var jsonPayload = JsonSerializer.Serialize(new
        {
            articleId = article.ArticleId,
            name = article.Name,
            itemNumber = article.ItemNumber,
            storageBin = article.StorageBin,
            groupId = article.Group.GroupId,
            operationAreaId = article.OperationArea.OperationAreaId,
            storagePlaceId = article.StoragePlace.StoragePlaceId,
            procurementId = article.Procurement.ProcurementId,
            stock = article.Stock,
            minStock = article.MinStock,
            imageUrl = article.ImageUrl,
            shoppingUrl = article.ShoppingUrl,
        });
        request.Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

        var response = await client.SendAsync(request);
        var responseText = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            return ExceptionConverter.ConvertDataAccessExceptions<bool>(new DataAccessException("The request failed.", (int)response.StatusCode, responseText));
        }

        return new DataAccessResponse<bool> { Data = true };
    }

    public async Task<DataAccessResponse<bool>> DeleteArticleAsync(Guid id)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, $"{_configuration["Endpoints:API"]}/Article/{id}");
        var client = _httpClient.CreateClient();

        var response = await client.SendAsync(request);
        var responseText = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            return ExceptionConverter.ConvertDataAccessExceptions<bool>(new DataAccessException("The request failed.", (int)response.StatusCode, responseText));
        }

        return new DataAccessResponse<bool> { Data = true };
    }
}
