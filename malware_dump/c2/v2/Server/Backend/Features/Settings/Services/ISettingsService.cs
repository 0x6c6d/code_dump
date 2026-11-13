using Backend.Features.Settings.Models;

namespace Backend.Features.Settings.Services;

public interface ISettingsService
{
    Task<ServiceResponse<SettingsModel>> GetSettingsAsync();
    Task<ServiceResponse<Guid>> CreateSettingsAsync(int port, string defaultCommand, int bufferSizeReceive);
    Task<ServiceResponse<bool>> UpdateSettingsAsync(SettingsModel settings);
}