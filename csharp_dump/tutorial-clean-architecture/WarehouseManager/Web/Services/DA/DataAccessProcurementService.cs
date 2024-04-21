using System.Text;

namespace Web.Services.DA;

public class DataAccessProcurementService : IDataAccessProcurementService
{
    private readonly IHttpClientFactory _httpClient;
    private readonly IConfiguration _configuration;

    public DataAccessProcurementService(IHttpClientFactory httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<DataAccessResponse<List<ProcurementModel>>> GetProcurementsAsync()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"{_configuration["Endpoints:API"]}/Procurement");
        var client = _httpClient.CreateClient();

        var response = await client.SendAsync(request);
        var responseText = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            var procurements = JsonSerializer.Deserialize<List<ProcurementModel>>(responseText);
            if (procurements == null)
            {
                return ExceptionConverter.ConvertDataAccessExceptions<List<ProcurementModel>>(new DataAccessException("The response was null which was not expected.", (int)response.StatusCode, responseText));
            }

            return new DataAccessResponse<List<ProcurementModel>> { Data = procurements };
        }
        else
        {
            return ExceptionConverter.ConvertDataAccessExceptions<List<ProcurementModel>>(new DataAccessException("The request failed.", (int)response.StatusCode, responseText));
        }
    }

    public async Task<DataAccessResponse<ProcurementModel>> GetProcurementAsync(Guid id)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"{_configuration["Endpoints:API"]}/Procurement/{id}");
        var client = _httpClient.CreateClient();

        var response = await client.SendAsync(request);
        var responseText = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            var procurement = JsonSerializer.Deserialize<ProcurementModel>(responseText);
            if (procurement == null)
            {
                return ExceptionConverter.ConvertDataAccessExceptions<ProcurementModel>(new DataAccessException("The response was null which was not expected.", (int)response.StatusCode, responseText));
            }

            return new DataAccessResponse<ProcurementModel> { Data = procurement };
        }
        else
        {
            return ExceptionConverter.ConvertDataAccessExceptions<ProcurementModel>(new DataAccessException("The request failed.", (int)response.StatusCode, responseText));
        }
    }

    public async Task<DataAccessResponse<Guid>> CreateProcurementAsync(ProcurementModel procurement)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, $"{_configuration["Endpoints:API"]}/Procurement");
        var client = _httpClient.CreateClient();

        var jsonPayload = JsonSerializer.Serialize(new { name = procurement.Name, email = procurement.Email, phone = procurement.Phone, link = procurement.Link });
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

    public async Task<DataAccessResponse<bool>> UpdateProcurementAsync(ProcurementModel procurement)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, $"{_configuration["Endpoints:API"]}/Procurement");
        var client = _httpClient.CreateClient();

        var jsonPayload = JsonSerializer.Serialize(new
        {
            id = procurement.ProcurementId,
            name = procurement.Name,
            email = procurement.Email,
            phone = procurement.Phone,
            link = procurement.Link
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

    public async Task<DataAccessResponse<bool>> DeleteProcurementAsync(Guid id)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, $"{_configuration["Endpoints:API"]}/Procurement/{id}");
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
