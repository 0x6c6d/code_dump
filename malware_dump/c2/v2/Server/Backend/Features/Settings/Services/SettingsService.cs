using Backend.Features.Settings.Models;
using Backend.Features.Settings.Operations.Create;
using Backend.Features.Settings.Operations.Read;
using Backend.Features.Settings.Operations.Update;

namespace Backend.Features.Settings.Services;

public class SettingsService(IMediator mediator) : ISettingsService
{
    public async Task<ServiceResponse<SettingsModel>> GetSettingsAsync()
    {
        try
        {
            var result = await mediator.Send(new ReadSettingsRequest());
            if (result != null)
            {
                var settings = SettingsMapper.ReadSettingsReturnToSettings(result);
                return new ServiceResponse<SettingsModel> { Data = settings };
            }
            
            // create default settings if settings table is empty
            var resultCreate = await CreateSettingsAsync(50000, "whoami", 4096);
            if (!resultCreate.Success || resultCreate.Data == Guid.Empty)
            {
                return new ServiceResponse<SettingsModel>
                {
                    Success = false,
                    Message = !string.IsNullOrEmpty(resultCreate.Message)
                        ? resultCreate.Message
                        : "Error while creating default settings"
                };
            }
            
            result = await mediator.Send(new ReadSettingsRequest());
            if (result == null)
            {
                return new ServiceResponse<SettingsModel>
                {
                    Success = false,
                    Message = "Error while creating default settings"
                };
            }

            var newSettings = SettingsMapper.ReadSettingsReturnToSettings(result);
            return new ServiceResponse<SettingsModel> { Data = newSettings };

        }
        catch (Exception ex)
        {
            return new ServiceResponse<SettingsModel>
            {
                Success = false,
                Message = ex.Message
            };
        }
    }
    
    public async Task<ServiceResponse<Guid>> CreateSettingsAsync(int port, string defaultCommand, int bufferSizeReceive)
    {
        try
        {
            var result = await mediator.Send(new CreateSettingsRequest() { Port = port, DefaultCommand = defaultCommand, BufferSizeReceive = bufferSizeReceive});
            if (result == Guid.Empty)
            {
                return new ServiceResponse<Guid>
                {
                    Success = false,
                    Message = "Unable to create settings."
                };
            }
            
            return new ServiceResponse<Guid> { Data = result };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<Guid>
            {
                Success = false,
                Message = ex.Message
            };
        }
    }
    
    public async Task<ServiceResponse<bool>> UpdateSettingsAsync(SettingsModel settings)
    {
        try
        {
            await mediator.Send(new UpdateSettingsRequest()
            {
                Id = settings.Id,
                Port = settings.Port,
                DefaultCommand = settings.DefaultCommand,
                BufferSizeReceive = settings.BufferSizeReceive
            });

            return new ServiceResponse<bool> { Data = true };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<bool>
            {
                Success = false,
                Message = ex.Message
            };
        }
    }
}