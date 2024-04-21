using System.Text;

namespace Web.Services.DA;

public class DataAccessEntityService<TEntity> : IDataAccessEntityService<TEntity>
{
    private readonly IHttpClientFactory _httpClient;
    private readonly IConfiguration _configuration;

    public DataAccessEntityService(IHttpClientFactory httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<DataAccessResponse<List<TEntity>>> GetEntitiesAsync(string apiRoute)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"{_configuration["Endpoints:API"]}/{apiRoute}");
        var client = _httpClient.CreateClient();

        var response = await client.SendAsync(request);
        var responseText = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            var entities = JsonSerializer.Deserialize<List<TEntity>>(responseText);
            if (entities == null)
            {
                return ExceptionConverter.ConvertDataAccessExceptions<List<TEntity>>(new DataAccessException("The response was null which was not expected.", (int)response.StatusCode, responseText));
            }

            return new DataAccessResponse<List<TEntity>> { Data = entities };
        }
        else
        {
            return ExceptionConverter.ConvertDataAccessExceptions<List<TEntity>>(new DataAccessException("The request failed.", (int)response.StatusCode, responseText));
        }
    }

    public async Task<DataAccessResponse<TEntity>> GetEntityAsync(string apiRoute, Guid id)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"{_configuration["Endpoints:API"]}/{apiRoute}/{id}");
        var client = _httpClient.CreateClient();

        var response = await client.SendAsync(request);
        var responseText = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            var entity = JsonSerializer.Deserialize<TEntity>(responseText);
            if (entity == null)
            {
                return ExceptionConverter.ConvertDataAccessExceptions<TEntity>(new DataAccessException("The response was null which was not expected.", (int)response.StatusCode, responseText));
            }

            return new DataAccessResponse<TEntity> { Data = entity };
        }
        else
        {
            return ExceptionConverter.ConvertDataAccessExceptions<TEntity>(new DataAccessException("The request failed.", (int)response.StatusCode, responseText));
        }
    }

    public async Task<DataAccessResponse<Guid>> CreateEntityAsync(string apiRoute, string name)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, $"{_configuration["Endpoints:API"]}/{apiRoute}");
        var client = _httpClient.CreateClient();

        var jsonPayload = JsonSerializer.Serialize(new { name = name });
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

    public async Task<DataAccessResponse<bool>> UpdateEntityAsync(string apiRoute, Guid id, string name)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, $"{_configuration["Endpoints:API"]}/{apiRoute}");
        var client = _httpClient.CreateClient();

        var jsonPayload = JsonSerializer.Serialize(new { id = id, name = name });
        request.Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

        var response = await client.SendAsync(request);
        var responseText = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            return ExceptionConverter.ConvertDataAccessExceptions<bool>(new DataAccessException("The request failed.", (int)response.StatusCode, responseText));
        }

        return new DataAccessResponse<bool> { Data = true };
    }

    public async Task<DataAccessResponse<bool>> DeleteEntityAsync(string apiRoute, Guid id)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, $"{_configuration["Endpoints:API"]}/{apiRoute}/{id}");
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