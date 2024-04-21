using Application.Features.Articles.Commands.Create;
using Application.Features.Articles.Commands.Delete;
using Application.Features.Articles.Commands.Update;
using Application.Features.Articles.Queries.GetArticle;
using Application.Features.Articles.Queries.GetArticles;

namespace Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class ArticleController : ControllerBase
{
    private readonly IMediator _mediator;

    public ArticleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet(Name = "GetArticles")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<List<GetArticlesVm>>> GetArticles()
    {
        var dtos = await _mediator.Send(new GetArticlesQuery());
        return Ok(dtos);
    }

    [HttpGet("{id}", Name = "GetArticle")]
    public async Task<ActionResult<GetArticleVm>> GetArticle(Guid id)
    {
        var getArticleQuery = new GetArticleQuery() { ArticleId = id };
        return Ok(await _mediator.Send(getArticleQuery));
    }

    [HttpPost(Name = "CreateArticle")]
    public async Task<ActionResult<Guid>> CreateArticle([FromBody] CreateArticleCommand createArticleCommand)
    {
        var id = await _mediator.Send(createArticleCommand);
        return Ok(id);
    }

    [HttpPut(Name = "UpdateArticle")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> UpdateArticle([FromBody] UpdateArticleCommand updateArticleCommand)
    {
        await _mediator.Send(updateArticleCommand);
        return NoContent();
    }

    [HttpDelete("{id}", Name = "DeleteArticle")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> DeleteArticle(Guid id)
    {
        var deleteArticleCommand = new DeleteArticleCommand() { ArticleId = id };
        await _mediator.Send(deleteArticleCommand);
        return NoContent();
    }
}