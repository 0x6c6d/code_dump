namespace Web.Components;

public partial class AdminProcurement
{
    public string MessageTop = string.Empty;
    public string MessageBottom = string.Empty;
    public ProcurementModel ProcurementNew = new();
    public ProcurementModel ProcurementSelected = new();
    public List<ProcurementModel> Procurements = new();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await GetProcurements();

        if (firstRender)
            StateHasChanged();
    }

    private void HandleChangeEvent(Guid procurementId)
    {
        ProcurementSelected.ProcurementId = procurementId;
        ProcurementSelected.Name = Procurements.FirstOrDefault(g => g.ProcurementId == procurementId)?.Name ?? string.Empty;
        ProcurementSelected.Email = Procurements.FirstOrDefault(g => g.ProcurementId == procurementId)?.Email ?? string.Empty;
        ProcurementSelected.Phone = Procurements.FirstOrDefault(g => g.ProcurementId == procurementId)?.Phone ?? string.Empty;
        ProcurementSelected.Link = Procurements.FirstOrDefault(g => g.ProcurementId == procurementId)?.Link ?? string.Empty;
    }

    protected async void CreateProcurement(ProcurementModel procurement)
    {
        MessageTop = string.Empty;
        MessageBottom = string.Empty;

        if (string.IsNullOrEmpty(procurement.Name) || string.IsNullOrEmpty(procurement.Email)
            || string.IsNullOrEmpty(procurement.Phone) || string.IsNullOrEmpty(procurement.Link))
        {
            MessageTop = "Bitte alle Felder ausfüllen.";
            return;
        }

        var result = await ProcurementService.CreateProcurement(procurement);
        if (!result.Success || result.Data == Guid.Empty)
        {
            MessageTop = $"{result.Message} {(string.IsNullOrEmpty(result.ValidationErrors) ? "" : result.ValidationErrors)}";
            StateHasChanged();
            return;
        }

        MessageTop = "Lieferanten erfolgreich angelegt.";
        await GetProcurements();
        ProcurementNew = new();
        StateHasChanged();
    }

    protected async void UpdateProcurement(ProcurementModel procurement)
    {
        MessageTop = string.Empty;
        MessageBottom = string.Empty;

        if (procurement.ProcurementId == Guid.Empty)
        {
            MessageBottom = "Bitte Lieferanten auswählen.";
            return;
        }

        if (string.IsNullOrEmpty(procurement.Name) || string.IsNullOrEmpty(procurement.Email)
            || string.IsNullOrEmpty(procurement.Phone) || string.IsNullOrEmpty(procurement.Link))
        {
            MessageBottom = "Bitte alle Felder ausfüllen.";
            return;
        }

        var result = await ProcurementService.UpdateProcurement(procurement);
        if (!result.Success)
        {
            MessageBottom = $"{result.Message} {(string.IsNullOrEmpty(result.ValidationErrors) ? "" : result.ValidationErrors)}";
            StateHasChanged();
            return;
        }

        MessageBottom = "Lieferantendaten erfolgreich geupdatet.";
        await GetProcurements();
        ProcurementSelected = Procurements.FirstOrDefault(x => x.ProcurementId == procurement.ProcurementId) ?? new();
        StateHasChanged();
    }

    protected async void DeleteProcurement(Guid procurementId)
    {
        MessageTop = string.Empty;
        MessageBottom = string.Empty;

        if (procurementId == Guid.Empty)
        {
            MessageBottom = "Bitte Lieferanten auswählen";
            return;
        }

        var result = await ProcurementService.DeleteProcurement(procurementId);
        if (!result.Success)
        {
            MessageBottom = $"{result.Message} {(string.IsNullOrEmpty(result.ValidationErrors) ? "" : result.ValidationErrors)}";
            StateHasChanged();
            return;
        }

        MessageBottom = "Lieferanten erfolgreich gelöscht.";
        ProcurementSelected = new();
        await GetProcurements();
        StateHasChanged();
    }

    private async Task GetProcurements()
    {
        var result = await ProcurementService.GetProcurements();
        if (!result.Success)
        {
            MessageTop += $"\n{result.Message}";
            return;
        }

        Procurements = result.Data ?? new();
    }
}
