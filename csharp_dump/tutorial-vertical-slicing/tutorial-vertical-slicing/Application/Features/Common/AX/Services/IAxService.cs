namespace Application.Features.Common.AX.Services;

public interface IAxService
{
    Task<ServiceResponse<bool>> CheckStoreId(string storeId);
    Task<ServiceResponse<List<string>>> GetStoresAsync();
    Task<ServiceResponse<string>> GetStoreDescription(string storeId);
    Task<ServiceResponse<StoreInformation>> GetStoreInformationAsync(string storeId);
}