using Application.Features.WarehouseManager.OperationAreas.Models;

namespace Presentation.Components.Pages.WarehouseManager.Shared;

public partial class OperationAreaSettings
{
    public string messageTop = string.Empty;
    public string messageBottom = string.Empty;
    public bool messageError = false;
    public OperationArea operationAreaNew = new();
    public OperationArea operationAreaSelected = new();
    public List<OperationArea> operationAreas = new();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await GetOperationAreas();
            StateHasChanged();
        }
    }

    private void HandleChangeEvent(Guid id)
    {
        if (id == Guid.Empty)
        {
            operationAreaSelected = new();
        }
        else
        {
            operationAreaSelected.Id = id;
            operationAreaSelected.Name = operationAreas.FirstOrDefault(g => g.Id == id)?.Name ?? string.Empty;
        }

        ResetMessage();
    }

    protected async Task CreateOperationArea(OperationArea operationArea)
    {
        ResetMessage();

        if (string.IsNullOrEmpty(operationArea.Name))
        {
            messageError = true;
            messageTop = "Bitte Namen eintragen.";
            return;
        }

        var result = await operationAreaService.CreateOperationAreaAsync(operationArea.Name);
        if (!result.Success || result.Data == Guid.Empty)
        {
            messageError = true;
            messageTop = !string.IsNullOrEmpty(result.Message) ? result.Message : "Fehler beim Erstellen des Einsatzgebietes.";
            return;
        }

        operationAreaNew = new();
        operationAreaSelected = new();
        messageTop = "Einsatzgebiet erfolgreich angelegt.";

        await GetOperationAreas();
        StateHasChanged();
    }

    protected async Task UpdateOperationArea(OperationArea operationArea)
    {
        ResetMessage();

        if (operationArea.Id == Guid.Empty)
        {
            messageError = true;
            messageBottom = "Bitte Einsatzgebiet auswählen.";
            return;
        }

        if (string.IsNullOrEmpty(operationArea.Name))
        {
            messageError = true;
            messageBottom = "Bitte neuen Namen vergeben.";
            return;
        }

        var result = await operationAreaService.UpdateOperationAreaAsync(operationArea.Id, operationArea.Name);
        if (!result.Success)
        {
            messageError = true;
            messageTop = !string.IsNullOrEmpty(result.Message) ? result.Message : "Fehler beim Updaten des Einsatzgebietes.";
            return;
        }

        messageBottom = "Einsatzgebiet erfolgreich geupdatet.";
        await GetOperationAreas();
        operationAreaSelected = operationAreas.FirstOrDefault(x => x.Id == operationArea.Id) ?? new();
        StateHasChanged();
    }

    protected async Task DeleteOperationArea(Guid id)
    {
        ResetMessage();

        if (id == Guid.Empty)
        {
            messageError = true;
            messageBottom = "Bitte Einsatzgebiet auswählen";
            return;
        }

        var result = await operationAreaService.DeleteOperationAreaAsync(id);
        if (!result.Success)
        {
            messageError = true;
            messageTop = !string.IsNullOrEmpty(result.Message) ? result.Message : "Fehler beim Löschen des Einsatzgebietes.";
            return;
        }

        messageBottom = "Einsatzgbiet erfolgreich gelöscht.";
        operationAreaSelected = new();
        await GetOperationAreas();
        StateHasChanged();
    }

    #region Helper
    private async Task GetOperationAreas()
    {
        var result = await operationAreaService.GetOperationAreasAsync();
        if (!result.Success || result.Data == null)
        {
            messageError = true;
            messageTop = !string.IsNullOrEmpty(result.Message) ? result.Message : "Fehler beim Laden der Einsatzgebiete.";
            return;
        }

        operationAreas = result.Data;
    }

    private void ResetMessage()
    {
        messageError = false;
        messageTop = string.Empty;
        messageBottom = string.Empty;
    }
    #endregion
}