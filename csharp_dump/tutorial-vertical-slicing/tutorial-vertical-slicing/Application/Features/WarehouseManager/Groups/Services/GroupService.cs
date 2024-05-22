using Application.Features.WarehouseManager.Groups.Models;
using Application.Features.WarehouseManager.Groups.Operations.Create;
using Application.Features.WarehouseManager.Groups.Operations.Delete;
using Application.Features.WarehouseManager.Groups.Operations.Read.All;
using Application.Features.WarehouseManager.Groups.Operations.Read.One;
using Application.Features.WarehouseManager.Groups.Operations.Update;

namespace Application.Features.WarehouseManager.Groups.Services;
public class GroupService : IGroupService
{
    private readonly IMediator _mediator;

    public GroupService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<ServiceResponse<Group>> GetGroupAsync(Guid id)
    {
        try
        {
            var result = await _mediator.Send(new GetGroupRequest { Id = id });
            if (result == null)
            {
                return new ServiceResponse<Group>
                {
                    Success = false,
                    Message = $"Keine Gruppe mit der ID {id} gefunden."
                };
            }

            var group = GroupMapper.GetGroupFromGetGroupReturn(result);
            return new ServiceResponse<Group> { Data = group };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<Group>
            {
                Success = false,
                Message = ex.Message
            };
        }
    }

    public async Task<ServiceResponse<List<Group>>> GetGroupsAsync()
    {
        try
        {
            var result = await _mediator.Send(new GetGroupsRequest());
            if (result == null)
            {
                return new ServiceResponse<List<Group>>
                {
                    Success = false,
                    Message = "Keine Gruppen gefunden."
                };
            }

            var groups = GroupMapper.GetGroupsFromGetGroupsReturn(result);
            return new ServiceResponse<List<Group>> { Data = groups };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<List<Group>>
            {
                Success = false,
                Message = ex.Message
            };
        }
    }

    public async Task<ServiceResponse<Guid>> CreateGroupAsync(string name)
    {
        try
        {
            var result = await _mediator.Send(new CreateGroupRequest() { Name = name });
            if (result == Guid.Empty)
            {
                return new ServiceResponse<Guid>
                {
                    Success = false,
                    Message = $"Fehler beim Erstellen der Gruppe."
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

    public async Task<ServiceResponse<bool>> UpdateGroupAsync(Guid id, string name)
    {
        try
        {
            await _mediator.Send(new UpdateGroupRequest() { Id = id, Name = name });
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

    public async Task<ServiceResponse<bool>> DeleteGroupAsync(Guid id)
    {
        try
        {
            await _mediator.Send(new DeleteGroupRequest() { Id = id });
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
