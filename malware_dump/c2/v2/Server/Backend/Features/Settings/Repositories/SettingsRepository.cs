using Backend.Common.Persistence;
using Backend.Common.Persistence.Repositories;
using Backend.Features.Settings.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.Settings.Repositories;

public class SettingsRepository(DataContext dbContext) : BaseRepository<SettingsModel>(dbContext), ISettingsRepository
{
    private readonly DataContext _dbContext = dbContext;

    public async Task<SettingsModel?> GetFirstOnlyAsync()
    {
        return await _dbContext.Settings.FirstOrDefaultAsync();
    }
    
    public async Task<bool> IsTableEmpty()
    {
        return await _dbContext.Settings.AnyAsync();
    }
}