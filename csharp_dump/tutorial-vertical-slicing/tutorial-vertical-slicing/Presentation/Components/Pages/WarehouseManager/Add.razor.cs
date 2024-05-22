using Application.Features.WarehouseManager.Articles;
using Application.Features.WarehouseManager.Articles.Models;
using Application.Features.WarehouseManager.Categories.Models;
using Application.Features.WarehouseManager.Groups.Models;
using Application.Features.WarehouseManager.OperationAreas.Models;
using Application.Features.WarehouseManager.Procurements.Models;
using Application.Features.WarehouseManager.StoragePlaces.Models;
using Microsoft.AspNetCore.Components;

namespace Presentation.Components.Pages.WarehouseManager;

public partial class Add
{
    private bool showDropdownList = false;
    private bool showStockInputFields = true;
    private bool showStoreInputFields = false;
    private bool showArticleNumbers = true;
    private string message = string.Empty;
    private string searchTerm = string.Empty;
    private Guid? withoutCategoryId;
    private ArticleVm articleVm = new();
    private List<Group> groups = new();
    private List<Category> categories = new();
    private List<StoragePlace> storagePlaces = new();
    private List<OperationArea> operationAreas = new();
    private List<Procurement> procurements = new();
    private List<string> stores = new();
    private List<string> filteredStores = new();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            var resultCategories = await categoryService.GetCategoriesAsync();
            if (resultCategories.Success && resultCategories.Data != null)
            {
                categories = resultCategories.Data;
                withoutCategoryId = categories.FirstOrDefault(x => x.Name.Equals("Ohne Kategorie", StringComparison.OrdinalIgnoreCase))?.Id ?? Guid.Empty;
                if (withoutCategoryId != Guid.Empty)
                {
                    articleVm.CategoryId = (Guid)withoutCategoryId;
                    showArticleNumbers = false;
                }

                // remove "Entsorgen" from category list
                var disposeId = categories.FirstOrDefault(x => x.Name.Equals("Entsorgt", StringComparison.OrdinalIgnoreCase))?.Id ?? Guid.Empty;
                categories.RemoveAll(category => category.Id == disposeId);
            }
            else if (!string.IsNullOrEmpty(resultCategories.Message))
            {
                message += resultCategories.Message + "\n";
            }

            var resultGroups = await groupService.GetGroupsAsync();
            if (resultGroups.Success && resultGroups.Data != null)
            {
                groups = resultGroups.Data;
            }
            else if (!string.IsNullOrEmpty(resultGroups.Message))
            {
                message += resultGroups.Message + "\n";
            }

            var resultOperationAreas = await operationAreaService.GetOperationAreasAsync();
            if (resultOperationAreas.Success && resultOperationAreas.Data != null)
            {
                operationAreas = resultOperationAreas.Data;
            }
            else if (!string.IsNullOrEmpty(resultOperationAreas.Message))
            {
                message += resultOperationAreas.Message + "\n";
            }

            var resultProcurements = await procurementService.GetProcurementsAsync();
            if (resultProcurements.Success && resultProcurements.Data != null)
            {
                procurements = resultProcurements.Data;
            }
            else if (!string.IsNullOrEmpty(resultProcurements.Message))
            {
                message += resultProcurements.Message + "\n";
            }

            var resultStoragePlace = await storagePlaceService.GetStoragePlacesAsync();
            if (resultStoragePlace.Success && resultStoragePlace.Data != null)
            {
                storagePlaces = resultStoragePlace.Data;
            }
            else if (!string.IsNullOrEmpty(resultStoragePlace.Message))
            {
                message += resultStoragePlace.Message + "\n";
            }

            var resultStores = await storeService.GetStoresAsync();
            if (resultStores.Success && resultStores.Data != null)
            {
                stores = resultStores.Data;
                filteredStores = stores;
            }
            else if (!string.IsNullOrEmpty(resultStores.Message))
            {
                message += resultStores.Message + "\n";
            }

            StateHasChanged();
        }
    }

    protected async Task HandleValidSubmit()
    {
        var article = ArticleMapper.ArticleVmToArticle(articleVm);
        var result = await articleService.CreateArticleAsync(article);
        if (!result.Success || result.Data == Guid.Empty)
        {
            message = $"Fehler beim Anlegen des Artikels.";
            return;
        }

        navManager.NavigateTo("/warehouse-manager");
    }

    #region Helper
    void FilterStores(ChangeEventArgs e)
    {
        searchTerm = e.Value?.ToString()?.ToLower() ?? string.Empty;
        filteredStores = stores.Where(store => store.ToLower().Contains(searchTerm)).ToList();
        showDropdownList = true;
    }

    void ShowDropdown()
    {
        showDropdownList = true;
    }

    void HideDropdown()
    {
        showDropdownList = false;
    }

    void SelectStore(string selectedStore)
    {
        articleVm.Store = selectedStore;
        searchTerm = selectedStore;
        showDropdownList = false;
    }

    void StoragePlaceInputChange(ChangeEventArgs e)
    {
        var guidInUse = storagePlaces.FirstOrDefault(x => x.Name.Contains("Im Einsatz"))?.Id ?? Guid.Empty;
        if (guidInUse == Guid.Empty)
        {
            return;
        }

        var selectedStoragePlace = string.IsNullOrEmpty(e.Value?.ToString()) ? Guid.Empty : new Guid(e.Value?.ToString() ?? string.Empty);
        if (guidInUse == selectedStoragePlace)
        {
            showStoreInputFields = true;
            articleVm.StoragePlace = "Im Einsatz";
            return;
        }

        showStoreInputFields = false;
        articleVm.Store = string.Empty;
        articleVm.StoragePlace = string.Empty;
    }

    void CategoryInputChange(ChangeEventArgs e)
    {
        var selectedCategoryId = new Guid(e.Value?.ToString() ?? string.Empty);
        if (selectedCategoryId == withoutCategoryId)
        {
            showStockInputFields = true;
            showArticleNumbers = false;
        }
        else
        {
            showStockInputFields = false;
            showArticleNumbers = true;
        }
    }
    #endregion
}
