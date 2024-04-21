using Application.Features.Procurements.Commands.Create;
using Application.Features.Procurements.Commands.Delete;
using Application.Features.Procurements.Commands.Update;
using Application.Features.Procurements.Queries.GetProcurement;
using Application.Features.Procurements.Queries.GetProcurements;

namespace Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class ProcurementController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProcurementController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet(Name = "GetProcurements")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<List<GetProcurementsVm>>> GetProcurements()
    {
        var dtos = await _mediator.Send(new GetProcurementsQuery());
        return Ok(dtos);
    }

    [HttpGet("{id}", Name = "GetProcurement")]
    public async Task<ActionResult<GetProcurementVm>> GetProcurement(Guid id)
    {
        var getProcurementQuery = new GetProcurementQuery() { ProcurementId = id };
        return Ok(await _mediator.Send(getProcurementQuery));
    }

    [HttpPost(Name = "CreateProcurement")]
    public async Task<ActionResult<Guid>> CreateProcurement([FromBody] CreateProcurementCommand createProcurementCommand)
    {
        var id = await _mediator.Send(createProcurementCommand);
        return Ok(id);
    }

    [HttpPut(Name = "UpdateProcurement")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> UpdateProcurement([FromBody] UpdateProcurementCommand updateProcurementCommand)
    {
        await _mediator.Send(updateProcurementCommand);
        return NoContent();
    }

    [HttpDelete("{id}", Name = "DeleteProcurement")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> DeleteProcurement(Guid id)
    {
        var deleteProcurementCommand = new DeleteProcurementCommand() { ProcurementId = id };
        await _mediator.Send(deleteProcurementCommand);
        return NoContent();
    }
}
