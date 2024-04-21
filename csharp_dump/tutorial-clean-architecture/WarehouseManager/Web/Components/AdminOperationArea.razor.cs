namespace Web.Components;

public partial class AdminOperationArea
{
    private string ApiRoute = "OperationArea";
    public string MessageTop = string.Empty;
    public string MessageBottom = string.Empty;
    public OperationAreaModel OperationAreaNew = new();
    public OperationAreaModel OperationAreaSelected = new();
    public List<OperationAreaModel> OperationAreas = new();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await GetOperationAreas();

        if (firstRender)
            StateHasChanged();
    }

    private void HandleChangeEvent(Guid operationAreaId)
    {
        OperationAreaSelected.OperationAreaId = operationAreaId;
        OperationAreaSelected.Name = OperationAreas.FirstOrDefault(g => g.OperationAreaId == operationAreaId)?.Name ?? string.Empty;
    }

    protected async Task CreateOperationArea(OperationAreaModel operationArea)
    {
        MessageTop = string.Empty;
        MessageBottom = string.Empty;

        if (string.IsNullOrEmpty(operationArea.Name))
        {
            MessageTop = "Bitte Namen eintragen.";
            return;
        }

        var result = await OperationAreaService.CreateEntity(ApiRoute, operationArea.Name);
        if (!result.Success || result.Data == Guid.Empty)
        {
            MessageTop = $"{result.Message} {(string.IsNullOrEmpty(result.ValidationErrors) ? "" : result.ValidationErrors)}";
            return;
        }

        MessageTop = "Einsatzgebiet erfolgreich angelegt.";
        await GetOperationAreas();
        OperationAreaNew = new();
        StateHasChanged();
    }

    protected async Task UpdateOperationArea(OperationAreaModel operationArea)
    {
        MessageTop = string.Empty;
        MessageBottom = string.Empty;

        if (operationArea.OperationAreaId == Guid.Empty)
        {
            MessageBottom = "Bitte Einsatzgebiet auswählen.";
            return;
        }

        if (string.IsNullOrEmpty(operationArea.Name))
        {
            MessageBottom = "Bitte neuen Namen vergeben.";
            return;
        }

        var result = await OperationAreaService.UpdateEntity(ApiRoute, operationArea.OperationAreaId, operationArea.Name);
        if (!result.Success)
        {
            MessageBottom = $"{result.Message} {(string.IsNullOrEmpty(result.ValidationErrors) ? "" : result.ValidationErrors)}";
            return;
        }

        MessageBottom = "Einsatzgebiet erfolgreich geupdatet.";
        await GetOperationAreas();
        OperationAreaSelected = OperationAreas.FirstOrDefault(x => x.OperationAreaId == operationArea.OperationAreaId) ?? new();
        StateHasChanged();
    }

    protected async Task DeleteOperationArea(Guid operationAreaId)
    {
        MessageTop = string.Empty;
        MessageBottom = string.Empty;

        if (operationAreaId == Guid.Empty)
        {
            MessageBottom = "Bitte Einsatzgebiet auswählen";
            return;
        }

        var result = await OperationAreaService.DeleteEntity(ApiRoute, operationAreaId);
        if (!result.Success)
        {
            MessageBottom = $"{result.Message} {(string.IsNullOrEmpty(result.ValidationErrors) ? "" : result.ValidationErrors)}";
            return;
        }

        MessageBottom = "Einsatzgebiet erfolgreich gelöscht.";
        OperationAreaSelected = new();
        await GetOperationAreas();
        StateHasChanged();
    }

    private async Task GetOperationAreas()
    {
        var result = await OperationAreaService.GetEntities(ApiRoute);
        if (!result.Success)
        {
            MessageTop += $"\n{result.Message}";
            return;
        }

        OperationAreas = result.Data ?? new();
    }
}