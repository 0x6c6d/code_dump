using Application.Features.WarehouseManager.Categories.Models;
using Application.Features.WarehouseManager.Categories.Operations.Create;
using Application.Features.WarehouseManager.Categories.Operations.Read.All;
using Application.Features.WarehouseManager.Categories.Operations.Read.One;
using Application.Features.WarehouseManager.Categories.Operations.Update;
using Riok.Mapperly.Abstractions;

namespace Application.Features.WarehouseManager.Categories;

[Mapper]
public static partial class CategoryMapper
{
    // MediatR
    public static partial GetCategoryReturn CategoryToGetCategoryReturn(Category category);

    public static partial List<GetCategoriesReturn> CategoriesToGetCategoriesReturn(IOrderedEnumerable<Category> categories);

    public static partial Category CreateCategoryRequestToCategory(CreateCategoryRequest createCategoryRequest);

    public static partial void UpdateCategoryRequestToCategory(UpdateCategoryRequest updateCategoryRequest, Category category);

    // Business Logic
    public static partial Category GetCategoryFromGetCategoryReturn(GetCategoryReturn getCategoryReturn);

    public static partial List<Category> GetCategoriesFromGetCategoriesReturn(List<GetCategoriesReturn> getCategoriesReturn);
}
