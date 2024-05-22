
using Application.Features.WarehouseManager.Categories.Models;

namespace Application.Features.WarehouseManager.Categories.Services;

public interface ICategoryService
{
    Task<ServiceResponse<Guid>> CreateCategoryAsync(string name);
    Task<ServiceResponse<bool>> DeleteCategoryAsync(Guid id, string oldName);
    Task<ServiceResponse<List<Category>>> GetCategoriesAsync();
    Task<ServiceResponse<Category>> GetCategoryAsync(Guid id);
    Task<ServiceResponse<bool>> UpdateCategoryAsync(Guid id, string oldName, string newName);
}