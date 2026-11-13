using Backend.Common.Persistence.Repositories;
using Backend.Features.Settings.Models;

namespace Backend.Features.Settings.Repositories;

public interface ISettingsRepository : IBaseRepository<SettingsModel>
{
    Task<SettingsModel?> GetFirstOnlyAsync();
    Task<bool> IsTableEmpty();
}