using Application.Features.Groups.Commands.Create;
using Application.Features.Groups.Commands.Delete;
using Application.Features.Groups.Commands.Update;
using Application.Features.Groups.Queries.GetGroup;
using Application.Features.Groups.Queries.GetGroups;

namespace Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class GroupController : ControllerBase
{
    private readonly IMediator _mediator;

    public GroupController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet(Name = "GetGroups")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<List<GetGroupsVm>>> GetGroups()
    {
        var dtos = await _mediator.Send(new GetGroupsQuery());
        return Ok(dtos);
    }

    [HttpGet("{id}", Name = "GetGroup")]
    public async Task<ActionResult<GetGroupVm>> GetGroup(Guid id)
    {
        var getGroupQuery = new GetGroupQuery() { GroupId = id };
        return Ok(await _mediator.Send(getGroupQuery));
    }

    [HttpPost(Name = "CreateGroup")]
    public async Task<ActionResult<Guid>> CreateGroup([FromBody] CreateGroupCommand createGroupCommand)
    {
        var id = await _mediator.Send(createGroupCommand);
        return Ok(id);
    }

    [HttpPut(Name = "UpdateGroup")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> UpdateGroup([FromBody] UpdateGroupCommand updateGroupCommand)
    {
        await _mediator.Send(updateGroupCommand);
        return NoContent();
    }

    [HttpDelete("{id}", Name = "DeleteGroup")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> DeleteGroup(Guid id)
    {
        var deleteGroupCommand = new DeleteGroupCommand() { GroupId = id };
        await _mediator.Send(deleteGroupCommand);
        return NoContent();
    }
}
