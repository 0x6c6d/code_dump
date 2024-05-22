using Application.Features.WarehouseManager.StoragePlaces.Models;

namespace Presentation.Components.Pages.WarehouseManager.Shared;

public partial class StoragePlaceSettings
{
    public string messageTop = string.Empty;
    public string messageBottom = string.Empty;
    private string selectedStoragePlaceName = string.Empty;
    public bool messageError = false;
    public StoragePlace storagePlaceNew = new();
    public StoragePlace storagePlaceSelected = new();
    public List<StoragePlace> storagePlaces = new();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await GetStoragePlaces();
            StateHasChanged();
        }
    }

    private void HandleChangeEvent(Guid id)
    {
        if (id == Guid.Empty)
        {
            storagePlaceSelected = new();
            selectedStoragePlaceName = string.Empty;
        }
        else
        {
            storagePlaceSelected.Id = id;
            storagePlaceSelected.Name = storagePlaces.FirstOrDefault(g => g.Id == id)?.Name ?? string.Empty;
            selectedStoragePlaceName = storagePlaceSelected.Name;
        }

        ResetMessage();
    }

    protected async Task CreateStoragePlace(StoragePlace storagePlace)
    {
        ResetMessage();

        if (string.IsNullOrEmpty(storagePlace.Name))
        {
            messageError = true;
            messageTop = "Bitte Namen eintragen.";
            return;
        }

        var result = await storagePlaceService.CreateStoragePlaceAsync(storagePlace.Name);
        if (!result.Success || result.Data == Guid.Empty)
        {
            messageError = true;
            messageTop = !string.IsNullOrEmpty(result.Message) ? result.Message : "Fehler beim Erstellen des Lagerplatzes.";
            return;
        }

        storagePlaceNew = new();
        storagePlaceSelected = new();
        messageTop = "Lagerplatz erfolgreich angelegt.";

        await GetStoragePlaces();
        StateHasChanged();
    }

    protected async Task UpdateStoragePlace(StoragePlace storagePlace)
    {
        ResetMessage();

        if (storagePlace.Id == Guid.Empty)
        {
            messageError = true;
            messageBottom = "Bitte Lagerplatz auswählen.";
            return;
        }

        if (string.IsNullOrEmpty(storagePlace.Name))
        {
            messageError = true;
            messageBottom = "Bitte neuen Namen vergeben.";
            return;
        }

        var result = await storagePlaceService.UpdateStoragePlaceAsync(storagePlace.Id, selectedStoragePlaceName, storagePlace.Name);
        if (!result.Success)
        {
            messageError = true;
            messageTop = !string.IsNullOrEmpty(result.Message) ? result.Message : "Fehler beim Updaten des Lagerplatzes.";
            return;
        }

        messageBottom = "Lagerplatz erfolgreich geupdatet.";
        await GetStoragePlaces();
        storagePlaceSelected = storagePlaces.FirstOrDefault(x => x.Id == storagePlace.Id) ?? new();
        StateHasChanged();
    }

    protected async Task DeleteGroup(Guid id)
    {
        ResetMessage();

        if (id == Guid.Empty)
        {
            messageError = true;
            messageBottom = "Bitte Lagerplatz auswählen";
            return;
        }

        var result = await storagePlaceService.DeleteStoragePlaceAsync(id, selectedStoragePlaceName);
        if (!result.Success)
        {
            messageError = true;
            messageTop = !string.IsNullOrEmpty(result.Message) ? result.Message : "Fehler beim Löschen des Lagerplatzes.";
            return;
        }

        messageBottom = "Lagerplatz erfolgreich gelöscht.";
        storagePlaceSelected = new();
        await GetStoragePlaces();
        StateHasChanged();
    }

    #region Helper
    private async Task GetStoragePlaces()
    {
        var result = await storagePlaceService.GetStoragePlacesAsync();
        if (!result.Success || result.Data == null)
        {
            messageError = true;
            messageTop = !string.IsNullOrEmpty(result.Message) ? result.Message : "Fehler beim Laden des Lagerplatzes.";
            return;
        }

        storagePlaces = result.Data;
    }

    private void ResetMessage()
    {
        messageError = false;
        messageTop = string.Empty;
        messageBottom = string.Empty;
    }
    #endregion
}