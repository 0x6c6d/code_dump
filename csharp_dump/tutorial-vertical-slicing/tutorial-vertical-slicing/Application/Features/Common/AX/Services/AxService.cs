using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace Application.Features.Common.AX.Services;
public class AxService : IAxService
{
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _httpClient;

    public AxService(IConfiguration configuration, IHttpClientFactory httpClient)
    {
        _configuration = configuration;
        _httpClient = httpClient;
    }

    public async Task<ServiceResponse<bool>> CheckStoreId(string storeId)
    {
        var client = _httpClient.CreateClient();
        var request = new HttpRequestMessage(HttpMethod.Get, _configuration["Endpoints:CheckStoreId"]+storeId);

        var response = await client.SendAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            return new ServiceResponse<bool>()
            {
                Success = false,
                Message = $"{response.StatusCode} HTTP Error beim Holen der Filialen aus AX"
            };
        }

        using var content = await response.Content.ReadAsStreamAsync();
        var result = await JsonSerializer.DeserializeAsync<ServiceResponse<bool>>(content);
        if (result == null || !result.Success)
        {
            return new ServiceResponse<bool>()
            {
                Success = false,
                Message = $"Fehler beim Holen der Filialen aus AX"
            };
        }

        return new ServiceResponse<bool>() { Data = result.Data };
    }

    public async Task<ServiceResponse<List<string>>> GetStoresAsync()
    {
        var client = _httpClient.CreateClient();
        var request = new HttpRequestMessage(HttpMethod.Get, _configuration["Endpoints:GetStores"]);

        var response = await client.SendAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            return new ServiceResponse<List<string>>()
            {
                Success = false,
                Message = $"{response.StatusCode} HTTP Error beim Holen der Filialen aus AX"
            };
        }

        using var content = await response.Content.ReadAsStreamAsync();
        var result = await JsonSerializer.DeserializeAsync<ServiceResponse<string[]>>(content);
        if (result == null || result.Data == null || !result.Success)
        {
            return new ServiceResponse<List<string>>()
            {
                Success = false,
                Message = $"Fehler beim Holen der Filialen aus AX"
            };
        }

        return new ServiceResponse<List<string>>() { Data = result.Data.ToList() };
    }

    public async Task<ServiceResponse<string>> GetStoreDescription(string storeId)
    {
        var client = _httpClient.CreateClient();
        var request = new HttpRequestMessage(HttpMethod.Get, _configuration["Endpoints:GetStoreDescription"] + storeId);

        var response = await client.SendAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            return new ServiceResponse<string>()
            {
                Success = false,
                Message = $"{response.StatusCode} HTTP Error beim Holen der Filialbeschreibung aus AX"
            };
        }

        using var content = await response.Content.ReadAsStreamAsync();
        var result = await JsonSerializer.DeserializeAsync<ServiceResponse<string>>(content);
        if (result == null || result.Data == null || !result.Success)
        {
            return new ServiceResponse<string>()
            {
                Success = false,
                Message = $"Fehler beim Holen der Filialbeschreibung aus AX"
            };
        }

        return new ServiceResponse<string>() { Data = result.Data };
    }

    public async Task<ServiceResponse<StoreInformation>> GetStoreInformationAsync(string storeId)
    {
        var client = _httpClient.CreateClient();
        var request = new HttpRequestMessage(HttpMethod.Get, _configuration["Endpoints:GetStoreInformation"] + storeId);

        var response = await client.SendAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            return new ServiceResponse<StoreInformation>()
            {
                Success = false,
                Message = $"{response.StatusCode} HTTP Error beim Holen der Filialinformationen aus AX"
            };
        }

        using var content = await response.Content.ReadAsStreamAsync();
        var result = await JsonSerializer.DeserializeAsync<ServiceResponse<string[]>>(content);
        if (result == null || result.Data == null || !result.Success)
        {
            return new ServiceResponse<StoreInformation>()
            {
                Success = false,
                Message = $"Fehler beim Holen der Filialinformationen aus AX"
            };
        }

        if (result.Data.Length != 22)
        {
            return new ServiceResponse<StoreInformation>()
            {
                Success = false,
                Message = $"Fehler beim Holen der Filialinformationen aus AX"
            };
        }

        var storeInfo = new StoreInformation()
        {
            StoreId = storeId,
            Name = result.Data[0],
            Center = result.Data[1],
            Street = result.Data[2],
            Zip = result.Data[3],
            City = result.Data[4],
            CountryLong = result.Data[5],
            CountryShort = result.Data[6],
            State = result.Data[7],
            StateShort = result.Data[8],
            SalesState = result.Data[9],
            SalesStateNumber = result.Data[10],
            StoreManager = result.Data[11],
            StoreManagerId = result.Data[12],
            RegionalManager = result.Data[13],
            RegionalManagerId = result.Data[14],
            DistrictManager = result.Data[15],
            DistrictManagerId = result.Data[16],
            StoreType = result.Data[19],
            Status = result.Data[20],
            Description = result.Data[21],
        };

        return new ServiceResponse<StoreInformation>() { Data = storeInfo };
    }
}
