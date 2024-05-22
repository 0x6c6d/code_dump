
using Application.Features.StoreManager.Technologies.Models;

namespace Application.Features.StoreManager.Technologies.Services;

public interface ITechnologyService
{
    Task<ServiceResponse<Technology>> GetTechnologyAsync(string storeId);
    Task<ServiceResponse<bool>> UpdateTechnologyAsync(Technology technology);
    Task<ServiceResponse<List<string>>> SearchTechnologyForString(string searchInput);
}