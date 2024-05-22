using Application.Features.WarehouseManager.Articles;
using Application.Features.WarehouseManager.Articles.Models;
using Application.Features.WarehouseManager.Categories.Models;
using Application.Features.WarehouseManager.Groups.Models;
using Application.Features.WarehouseManager.OperationAreas.Models;
using Application.Features.WarehouseManager.Procurements.Models;
using Application.Features.WarehouseManager.StoragePlaces.Models;
using Microsoft.AspNetCore.Components;

namespace Presentation.Components.Pages.WarehouseManager.Shared;

public partial class Details
{
    [Parameter] public EventCallback<Article> OnUpdate { get; set; }

    private bool showModal = false;
    private bool showDeletePage = false;
    private bool showDropdownList = false;
    private bool showStockInputFields = false;
    private bool showStoreInputFields = false;
    private bool showArticleNumbers = true;
    private bool articleIsDisposed = false;
    private string message = string.Empty;
    private string searchTerm = string.Empty;
    private Guid disposeId;
    private DateTime? disposedAt;
    private ArticleVm articleVm = new();
    private List<Group> groups = new();
    private List<Category> categories = new();
    private List<StoragePlace> storagePlaces = new();
    private List<OperationArea> operationAreas = new();
    private List<Procurement> procurements = new();
    private List<string> stores = new();
    private List<string> filteredStores = new();

    private async Task LoadData()
    {
        var resultGroup = await groupService.GetGroupsAsync();
        groups = resultGroup.Data ?? new();

        var resultCategory = await categoryService.GetCategoriesAsync();
        categories = resultCategory.Data ?? new();

        var resultStoragePlace = await storagePlaceService.GetStoragePlacesAsync();
        storagePlaces = resultStoragePlace.Data ?? new();

        var resultOperationArea = await operationAreaService.GetOperationAreasAsync();
        operationAreas = resultOperationArea.Data ?? new();

        var resultProcurements = await procurementService.GetProcurementsAsync();
        procurements = resultProcurements.Data ?? new();

        var resultStores = await storeService.GetStoresAsync();
        stores = resultStores.Data ?? new();
        filteredStores = resultStores.Data ?? new();
    }

    protected async Task HandleValidSubmit()
    {
        var article = ArticleMapper.ArticleVmToArticle(articleVm);
        var result = await articleService.UpdateArticleAsync(article);
        if (!result.Success)
        {
            message = !string.IsNullOrEmpty(result.Message) ? result.Message : "Fehler beim updaten des Artikels.";
            return;
        }

        var resultUpdate = await articleService.GetArticleAsync(articleVm.Id);
        if (!resultUpdate.Success)
        {
            message = !string.IsNullOrEmpty(result.Message) ? result.Message : "Fehler beim Abfragen des geupdateten Artikels von der Datenbank.";
            return;
        }

        await OnUpdate.InvokeAsync(resultUpdate.Data);
        CloseModal();
    }

    private async void HandleDispose()
    {
        if (disposeId == Guid.Empty)
        {
            message = "Die CategoryId für die Kategorie 'Entsorgt' konnte nicht gefunden werden.\nDer Artikel wurde nicht gelöscht.";
            return;
        }

        articleVm.CategoryId = disposeId;
        var article = ArticleMapper.ArticleVmToArticle(articleVm);
        article.Disposed = DateTime.Now;
        article.Category.Name = "Entsorgt";

        var result = await articleService.UpdateArticleAsync(article);
        if (!result.Success || !result.Data)
        {
            message = "Fehler beim Löschen des Artikels.";
            return;
        }

        await OnUpdate.InvokeAsync(article);
        CloseModal();
    }

    #region Helper
    public async Task OpenModal(Article article)
    {
        message = string.Empty;
        showStoreInputFields = false;
        showStockInputFields = false;
        showArticleNumbers = true;
        articleIsDisposed = false;

        articleVm = ArticleMapper.ArticleToArticleVm(article);
        searchTerm = articleVm.Store;
        disposedAt = article.Disposed;

        if (article.StoragePlace.Name.Equals("Im Einsatz", StringComparison.CurrentCultureIgnoreCase))
        {
            showStoreInputFields = true;
        }
        if (article.Category.Name.Equals("Ohne Kategorie", StringComparison.CurrentCultureIgnoreCase))
        {
            showStockInputFields = true;
            showArticleNumbers = false;
        }

        showModal = true;
        StateHasChanged();
        await LoadData();


        // remove "Entsorgen" from category list or don't display the buttons
        disposeId = categories.FirstOrDefault(x => x.Name.Equals("Entsorgt", StringComparison.OrdinalIgnoreCase))?.Id ?? Guid.Empty;
        if (articleVm.CategoryId != disposeId)
        {
            categories.RemoveAll(category => category.Id == disposeId);
        }
        else
        {
            articleIsDisposed = true;
        }

        StateHasChanged();
    }

    private void CloseModal()
    {
        filteredStores = stores;
        showDeletePage = false;
        showModal = false;
        StateHasChanged();
    }
    #endregion

    #region StoreFilter
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

    void StoragePlaceInputChange(ChangeEventArgs e)
    {
        var id = storagePlaces.FirstOrDefault(x => x.Name.Equals("Im Einsatz", StringComparison.OrdinalIgnoreCase))?.Id;
        if (id == Guid.Empty)
        {
            return;
        }

        var selectedStoragePlace = string.IsNullOrEmpty(e.Value?.ToString()) ? Guid.Empty : new Guid(e.Value?.ToString() ?? string.Empty);
        if (id == selectedStoragePlace)
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
        var id = categories.FirstOrDefault(x => x.Name.Equals("Ohne Kategorie", StringComparison.OrdinalIgnoreCase))?.Id;
        if (id == Guid.Empty)
        {
            return;
        }

        var selectedStoragePlace = string.IsNullOrEmpty(e.Value?.ToString()) ? Guid.Empty : new Guid(e.Value?.ToString() ?? string.Empty);
        if (id == selectedStoragePlace)
        {
            showStockInputFields = true;
            showArticleNumbers = false;
            articleVm.StoragePlace = "Ohne Kategorie";
            articleVm.InventoryNumber = string.Empty;
            articleVm.SerialNumber = string.Empty;
            return;
        }

        showStockInputFields = false;
        showArticleNumbers = true;
        articleVm.StoragePlace = string.Empty;
    }

    void SelectStore(string selectedStore)
    {
        articleVm.Store = selectedStore;
        searchTerm = selectedStore;
        showDropdownList = false;
    }
    #endregion
}
