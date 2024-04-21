namespace Web.Components;

public partial class AdminStoragePlace
{
    private string ApiRoute = "StoragePlace";
    public string MessageTop = string.Empty;
    public string MessageBottom = string.Empty;
    public StoragePlaceModel StoragePlaceNew = new();
    public StoragePlaceModel StoragePlaceSelected = new();
    public List<StoragePlaceModel> StoragePlaces { get; set; } = new();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await GetStoragePlaces();

        if (firstRender)
            StateHasChanged();
    }

    private void HandleChangeEvent(Guid storagePlaceId)
    {
        StoragePlaceSelected.StoragePlaceId = storagePlaceId;
        StoragePlaceSelected.Name = StoragePlaces.FirstOrDefault(g => g.StoragePlaceId == storagePlaceId)?.Name ?? string.Empty;
    }

    protected async Task CreateStoragePlace(StoragePlaceModel storagePlace)
    {
        MessageTop = string.Empty;
        MessageBottom = string.Empty;

        if (string.IsNullOrEmpty(storagePlace.Name))
        {
            MessageTop = "Bitte Namen eintragen.";
            return;
        }

        var result = await StoragePlaceService.CreateEntity(ApiRoute, storagePlace.Name);
        if (!result.Success || result.Data == Guid.Empty)
        {
            MessageTop = $"{result.Message} {(string.IsNullOrEmpty(result.ValidationErrors) ? "" : result.ValidationErrors)}";
            return;
        }

        MessageTop = "Lagerplatz erfolgreich angelegt.";
        await GetStoragePlaces();
        StoragePlaceNew = new();
        StateHasChanged();
    }

    protected async Task UpdateStoragePlace(StoragePlaceModel storagePlace)
    {
        MessageTop = string.Empty;
        MessageBottom = string.Empty;

        if (storagePlace.StoragePlaceId == Guid.Empty)
        {
            MessageBottom = "Bitte Lagerort auswählen.";
            return;
        }

        if (string.IsNullOrEmpty(storagePlace.Name))
        {
            MessageBottom = "Bitte neuen Namen vergeben.";
            return;
        }

        var result = await StoragePlaceService.UpdateEntity(ApiRoute, storagePlace.StoragePlaceId, storagePlace.Name);
        if (!result.Success)
        {
            MessageBottom = $"{result.Message} {(string.IsNullOrEmpty(result.ValidationErrors) ? "" : result.ValidationErrors)}";
            return;
        }

        MessageBottom = "Lagerort erfolgreich geupdatet.";
        await GetStoragePlaces();
        StoragePlaceSelected = StoragePlaces.FirstOrDefault(x => x.StoragePlaceId == storagePlace.StoragePlaceId) ?? new();
        StateHasChanged();
    }

    protected async Task DeleteStoragePlace(Guid storagePlaceId)
    {
        MessageTop = string.Empty;
        MessageBottom = string.Empty;

        if (storagePlaceId == Guid.Empty)
        {
            MessageTop = string.Empty;
            MessageBottom = "Bitte Lagerort auswählen";
            return;
        }

        var result = await StoragePlaceService.DeleteEntity(ApiRoute, storagePlaceId);
        if (!result.Success)
        {
            MessageBottom = $"{result.Message} {(string.IsNullOrEmpty(result.ValidationErrors) ? "" : result.ValidationErrors)}";
            return;
        }


        MessageBottom = "Lagerort erfolgreich gelöscht.";
        StoragePlaceSelected = new();
        await GetStoragePlaces();
        StateHasChanged();
    }

    private async Task GetStoragePlaces()
    {
        var result = await StoragePlaceService.GetEntities(ApiRoute);
        if (!result.Success)
        {
            MessageTop += $"\n{result.Message}";
            return;
        }

        StoragePlaces = result.Data ?? new();
    }
}
