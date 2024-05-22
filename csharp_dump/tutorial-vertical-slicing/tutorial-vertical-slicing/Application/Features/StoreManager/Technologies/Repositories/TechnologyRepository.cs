using Application.Features.StoreManager.Technologies.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.StoreManager.Technologies.Repositories;
public class TechnologyRepository : BaseRepository<Technology>, ITechnologyRepository
{
    public TechnologyRepository(DataContext dbContext) : base(dbContext) { }

    public Task<bool> IsStoreIdUnique(string storeId)
    {
        var match = _dbContext.Technologies.Any(a => a.StoreId.ToLower() == storeId.ToLower());
        return Task.FromResult(match);
    }

    public async Task<Technology?> GetByStoreIdAsync(string storeId)
    {
        var technology = await _dbContext.Technologies.FirstOrDefaultAsync(s => s.StoreId == storeId);
        return technology;
    }

    public List<string> SearchTechnologyForString(string searchInput)
    {
        // calls a stored procedure on the database
        var result = _dbContext.Database
            .SqlQueryRaw<string>("SearchTechnologies @SearchInput", new SqlParameter("@SearchInput", searchInput))
            .ToList();

        return result;
    }
}
