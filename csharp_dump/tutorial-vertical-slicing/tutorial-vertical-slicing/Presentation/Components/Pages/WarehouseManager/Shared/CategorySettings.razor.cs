using Application.Features.WarehouseManager.Categories.Models;

namespace Presentation.Components.Pages.WarehouseManager.Shared;

public partial class CategorySettings
{
    public string messageTop = string.Empty;
    public string messageBottom = string.Empty;
    private string selectedCategoryName = string.Empty;
    public bool messageError = false;
    public Category categoryNew = new();
    public Category categorySelected = new();
    public List<Category> categories = new();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await GetCategories();
            StateHasChanged();
        }
    }

    private void HandleChangeEvent(Guid id)
    {
        if (id == Guid.Empty)
        {
            categorySelected = new();
            selectedCategoryName = string.Empty;
        }
        else
        {
            categorySelected.Id = id;
            categorySelected.Name = categories.FirstOrDefault(g => g.Id == id)?.Name ?? string.Empty;
            selectedCategoryName = categorySelected.Name;
        }

        ResetMessage();
    }

    protected async Task CreateCategory(Category category)
    {
        ResetMessage();

        if (string.IsNullOrEmpty(category.Name))
        {
            messageError = true;
            messageTop = "Bitte Namen eintragen.";
            return;
        }

        var result = await categoryService.CreateCategoryAsync(category.Name);
        if (!result.Success || result.Data == Guid.Empty)
        {
            messageError = true;
            messageTop = !string.IsNullOrEmpty(result.Message) ? result.Message : "Fehler beim Erstellen der Kategorie.";
            return;
        }

        categoryNew = new();
        categorySelected = new();
        messageTop = "Kategorie erfolgreich angelegt.";

        await GetCategories();
        StateHasChanged();
    }

    protected async Task UpdateCategory(Category category)
    {
        ResetMessage();

        if (category.Id == Guid.Empty)
        {
            messageError = true;
            messageBottom = "Bitte Kategorie auswählen.";
            return;
        }

        if (string.IsNullOrEmpty(category.Name))
        {
            messageError = true;
            messageBottom = "Bitte neuen Namen vergeben.";
            return;
        }

        var result = await categoryService.UpdateCategoryAsync(category.Id, selectedCategoryName, category.Name);
        if (!result.Success)
        {
            messageError = true;
            messageTop = !string.IsNullOrEmpty(result.Message) ? result.Message : "Fehler beim Updaten der Kategorie.";
            return;
        }

        messageBottom = "Kategorie erfolgreich geupdatet.";
        await GetCategories();
        categorySelected = categories.FirstOrDefault(x => x.Id == category.Id) ?? new();
        StateHasChanged();
    }

    protected async Task DeleteCategory(Guid id)
    {
        ResetMessage();

        if (id == Guid.Empty)
        {
            messageError = true;
            messageBottom = "Bitte Kategorie auswählen";
            return;
        }

        var result = await categoryService.DeleteCategoryAsync(id, selectedCategoryName);
        if (!result.Success)
        {
            messageError = true;
            messageTop = !string.IsNullOrEmpty(result.Message) ? result.Message : "Fehler beim Löschen der Kategorie.";
            return;
        }

        messageBottom = "Kategorie erfolgreich gelöscht.";
        categorySelected = new();
        await GetCategories();
        StateHasChanged();
    }

    #region Helper
    private async Task GetCategories()
    {
        var result = await categoryService.GetCategoriesAsync();
        if (!result.Success || result.Data == null)
        {
            messageError = true;
            messageTop = !string.IsNullOrEmpty(result.Message) ? result.Message : "Fehler beim Laden der Kategorien.";
            return;
        }

        categories = result.Data;
    }

    private void ResetMessage()
    {
        messageError = false;
        messageTop = string.Empty;
        messageBottom = string.Empty;
    }
    #endregion
}