using Application.Features.WarehouseManager.Procurements.Models;

namespace Presentation.Components.Pages.WarehouseManager.Shared;

public partial class ProcurementDetails
{
    private bool showModal = false;
    private string message = string.Empty;
    private Procurement procurement = new();

    #region Helper
    public void OpenModal(Procurement procurement)
    {
        this.procurement = procurement;
        showModal = true;
        StateHasChanged();
    }

    private void CloseModal()
    {
        showModal = false;
        StateHasChanged();
    }
    #endregion
}
