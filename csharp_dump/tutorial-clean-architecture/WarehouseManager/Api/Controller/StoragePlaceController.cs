using Application.Features.StoragePlaces.Commands.Create;
using Application.Features.StoragePlaces.Commands.Delete;
using Application.Features.StoragePlaces.Commands.Update;
using Application.Features.StoragePlaces.Queries.GetStoragePlace;
using Application.Features.StoragePlaces.Queries.GetStoragePlaces;

namespace Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class StoragePlaceController : ControllerBase
{
    private readonly IMediator _mediator;

    public StoragePlaceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet(Name = "GetStoragePlaces")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<List<GetStoragePlacesVm>>> GetStoragePlaces()
    {
        var dtos = await _mediator.Send(new GetStoragePlacesQuery());
        return Ok(dtos);
    }

    [HttpGet("{id}", Name = "GetStoragePlace")]
    public async Task<ActionResult<GetStoragePlaceVm>> GetStoragePlace(Guid id)
    {
        var getStoragePlaceQuery = new GetStoragePlaceQuery() { StoragePlaceId = id };
        return Ok(await _mediator.Send(getStoragePlaceQuery));
    }

    [HttpPost(Name = "CreateStoragePlace")]
    public async Task<ActionResult<Guid>> CreateStoragePlace([FromBody] CreateStoragePlaceCommand createStoragePlaceCommand)
    {
        var id = await _mediator.Send(createStoragePlaceCommand);
        return Ok(id);
    }

    [HttpPut(Name = "UpdateStoragePlace")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> UpdateStoragePlace([FromBody] UpdateStoragePlaceCommand updateStoragePlaceCommand)
    {
        await _mediator.Send(updateStoragePlaceCommand);
        return NoContent();
    }

    [HttpDelete("{id}", Name = "DeleteStoragePlace")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> DeleteStoragePlace(Guid id)
    {
        var deleteStoragePlaceCommand = new DeleteStoragePlaceCommand() { StoragePlaceId = id };
        await _mediator.Send(deleteStoragePlaceCommand);
        return NoContent();
    }
}
