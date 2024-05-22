using Application.Features.WarehouseManager.Categories.Models;
using Application.Features.WarehouseManager.Categories.Operations.Create;
using Application.Features.WarehouseManager.Categories.Operations.Delete;
using Application.Features.WarehouseManager.Categories.Operations.Read.All;
using Application.Features.WarehouseManager.Categories.Operations.Read.One;
using Application.Features.WarehouseManager.Categories.Operations.Update;

namespace Application.Features.WarehouseManager.Categories.Services;
public class CategoryService : ICategoryService
{
    private readonly IMediator _mediator;

    public CategoryService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<ServiceResponse<Category>> GetCategoryAsync(Guid id)
    {
        try
        {
            var result = await _mediator.Send(new GetCategoryRequest { Id = id });
            if (result == null)
            {
                return new ServiceResponse<Category>
                {
                    Success = false,
                    Message = $"Keine Kategorie mit der ID {id} gefunden."
                };
            }

            var category = CategoryMapper.GetCategoryFromGetCategoryReturn(result);
            return new ServiceResponse<Category> { Data = category };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<Category>
            {
                Success = false,
                Message = ex.Message
            };
        }
    }

    public async Task<ServiceResponse<List<Category>>> GetCategoriesAsync()
    {
        try
        {
            var result = await _mediator.Send(new GetCategoriesRequest());
            if (result == null)
            {
                return new ServiceResponse<List<Category>>
                {
                    Success = false,
                    Message = "Keine Kategorien gefunden."
                };
            }

            var categories = CategoryMapper.GetCategoriesFromGetCategoriesReturn(result);
            return new ServiceResponse<List<Category>> { Data = categories };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<List<Category>>
            {
                Success = false,
                Message = ex.Message
            };
        }
    }

    public async Task<ServiceResponse<Guid>> CreateCategoryAsync(string name)
    {
        try
        {
            var result = await _mediator.Send(new CreateCategoryRequest() { Name = name });
            if (result == Guid.Empty)
            {
                return new ServiceResponse<Guid>
                {
                    Success = false,
                    Message = $"Fehler beim Erstellen der Kategorie."
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

    public async Task<ServiceResponse<bool>> UpdateCategoryAsync(Guid id, string oldName, string newName)
    {
        try
        {
            if (oldName.Equals("Ohne Kategorie", StringComparison.CurrentCultureIgnoreCase) ||
                oldName.Equals("Entsorgt", StringComparison.CurrentCultureIgnoreCase))
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = $"'{oldName}' kann nicht geupdated werden."
                };
            }

            await _mediator.Send(new UpdateCategoryRequest() { Id = id, Name = newName });
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

    public async Task<ServiceResponse<bool>> DeleteCategoryAsync(Guid id, string oldName)
    {
        try
        {
            if (oldName.Equals("Ohne Kategorie", StringComparison.CurrentCultureIgnoreCase) ||
                oldName.Equals("Entsorgt", StringComparison.CurrentCultureIgnoreCase))
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = $"'{oldName}' kann nicht gelöscht werden."
                };
            }

            await _mediator.Send(new DeleteCategoryRequest() { Id = id });
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
