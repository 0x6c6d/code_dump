using Application.Features.OperationAreas.Commands.Create;
using Application.Features.OperationAreas.Commands.Delete;
using Application.Features.OperationAreas.Commands.Update;
using Application.Features.OperationAreas.Queries.GetOperationArea;
using Application.Features.OperationAreas.Queries.GetOperationAreas;

namespace Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class OperationAreaController : ControllerBase
{
    private readonly IMediator _mediator;

    public OperationAreaController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet(Name = "GetOperationAreas")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<List<GetOperationAreaVm>>> GetOperationAreas()
    {
        var dtos = await _mediator.Send(new GetOperationAreasQuery());
        return Ok(dtos);
    }

    [HttpGet("{id}", Name = "GetOperationArea")]
    public async Task<ActionResult<GetOperationAreaVm>> GetOperationArea(Guid id)
    {
        var getGroupQuery = new GetOperationAreaQuery() { OperationAreaId = id };
        return Ok(await _mediator.Send(getGroupQuery));
    }

    [HttpPost(Name = "CreateOperationArea")]
    public async Task<ActionResult<Guid>> CreateOperationArea([FromBody] CreateOperationAreaCommand createOperationAreaCommand)
    {
        var id = await _mediator.Send(createOperationAreaCommand);
        return Ok(id);
    }

    [HttpPut(Name = "UpdateOperationArea")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> UpdateOperationArea([FromBody] UpdateOperationAreaCommand updateOperationAreaCommand)
    {
        await _mediator.Send(updateOperationAreaCommand);
        return NoContent();
    }

    [HttpDelete("{id}", Name = "DeleteOperationArea")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> DeleteOperationArea(Guid id)
    {
        var deleteOperationAreaCommand = new DeleteOperationAreaCommand() { OperationAreaId = id };
        await _mediator.Send(deleteOperationAreaCommand);
        return NoContent();
    }
}
