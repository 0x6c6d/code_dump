using Application.Features.WarehouseManager.Procurements.Models;

namespace Presentation.Components.Pages.WarehouseManager.Shared;

public partial class ProcurementSettings
{
    public string messageTop = string.Empty;
    public string messageBottom = string.Empty;
    public bool messageError = false;
    public Procurement procurementNew = new();
    public Procurement procurementSelected = new();
    public List<Procurement> procurements = new();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await GetProcurements();
            StateHasChanged();
        }
    }

    private void HandleChangeEvent(Guid id)
    {
        if (id == Guid.Empty)
        {
            procurementSelected = new();
        }
        else
        {
            procurementSelected.Id = id;
            procurementSelected.Name = procurements.FirstOrDefault(g => g.Id == id)?.Name ?? string.Empty;
            procurementSelected.Link = procurements.FirstOrDefault(g => g.Id == id)?.Link ?? string.Empty;
            procurementSelected.SalesName = procurements.FirstOrDefault(g => g.Id == id)?.SalesName ?? string.Empty;
            procurementSelected.SalesEmail = procurements.FirstOrDefault(g => g.Id == id)?.SalesEmail ?? string.Empty;
            procurementSelected.SalesPhone = procurements.FirstOrDefault(g => g.Id == id)?.SalesPhone ?? string.Empty;
            procurementSelected.SupportName = procurements.FirstOrDefault(g => g.Id == id)?.SupportName ?? string.Empty;
            procurementSelected.SupportEmail = procurements.FirstOrDefault(g => g.Id == id)?.SupportEmail ?? string.Empty;
            procurementSelected.SupportPhone = procurements.FirstOrDefault(g => g.Id == id)?.SupportPhone ?? string.Empty;
        }

        ResetMessage();
    }

    protected async Task CreateProcurement(Procurement procurement)
    {
        ResetMessage();

        if (string.IsNullOrEmpty(procurement.Name))
        {
            messageError = true;
            messageTop = "Bitte Namen eintragen.";
            return;
        }

        var result = await procurementService.CreateProcurementAsync(procurement);
        if (!result.Success || result.Data == Guid.Empty)
        {
            messageError = true;
            messageTop = !string.IsNullOrEmpty(result.Message) ? result.Message : "Fehler beim Erstellen des Lieferanten.";
            return;
        }

        procurementNew = new();
        procurementSelected = new();
        messageTop = "Lieferant erfolgreich angelegt.";

        await GetProcurements();
        StateHasChanged();
    }

    protected async Task UpdateProcurement(Procurement procurement)
    {
        ResetMessage();

        if (procurement.Id == Guid.Empty)
        {
            messageError = true;
            messageBottom = "Bitte Lieferanten auswählen.";
            return;
        }

        if (string.IsNullOrEmpty(procurement.Name))
        {
            messageError = true;
            messageBottom = "Bitte neuen Namen vergeben.";
            return;
        }

        var result = await procurementService.UpdateProcurementAsync(procurement);
        if (!result.Success)
        {
            messageError = true;
            messageBottom = !string.IsNullOrEmpty(result.Message) ? result.Message : "Fehler beim Updaten des Lieferanten.";
            return;
        }

        messageBottom = "Lieferanten erfolgreich geupdatet.";
        await GetProcurements();
        procurementSelected = procurements.FirstOrDefault(x => x.Id == procurement.Id) ?? new();
        StateHasChanged();
    }

    protected async Task DeleteProcurement(Guid id)
    {
        ResetMessage();

        if (id == Guid.Empty)
        {
            messageError = true;
            messageBottom = "Bitte Lieferanten auswählen";
            return;
        }

        var result = await procurementService.DeleteProcurementAsync(id);
        if (!result.Success)
        {
            messageError = true;
            messageTop = !string.IsNullOrEmpty(result.Message) ? result.Message : "Fehler beim Löschen des Lieferanten.";
            return;
        }

        messageBottom = "Lieferanten erfolgreich gelöscht.";
        procurementSelected = new();
        await GetProcurements();
        StateHasChanged();
    }

    #region Helper
    private async Task GetProcurements()
    {
        var result = await procurementService.GetProcurementsAsync();
        if (!result.Success || result.Data == null)
        {
            messageError = true;
            messageTop = !string.IsNullOrEmpty(result.Message) ? result.Message : "Fehler beim Laden der Lieferanten.";
            return;
        }

        procurements = result.Data;
    }

    private void ResetMessage()
    {
        messageError = false;
        messageTop = string.Empty;
        messageBottom = string.Empty;
    }
    #endregion
}