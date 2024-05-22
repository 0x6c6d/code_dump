using Application.Features.WarehouseManager.Categories.Models;

namespace Application.Features.WarehouseManager.Categories.Repositories;
public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(DataContext dbContext) : base(dbContext) { }

    public Task<bool> IsCategoryNameUnique(string name)
    {
        var match = _dbContext.Categories.Any(a => a.Name.Equals(name));
        return Task.FromResult(match);
    }
}
