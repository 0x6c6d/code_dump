using Application.Features.WarehouseManager.Categories.Models;

namespace Application.Features.WarehouseManager.Categories.Repositories;

public interface ICategoryRepository : IRepository<Category>
{
    Task<bool> IsCategoryNameUnique(string name);
}