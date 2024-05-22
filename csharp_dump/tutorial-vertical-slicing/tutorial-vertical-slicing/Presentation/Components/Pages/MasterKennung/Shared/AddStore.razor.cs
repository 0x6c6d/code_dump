using Application.Features.StoreManager.Stores.Models;
using Microsoft.AspNetCore.Components;

namespace Presentation.Components.Pages.MasterKennung.Shared;

public partial class AddStore
{
    [Parameter] public EventCallback<string> OnAdd { get; set; }

    private bool showModal = false;
    private string message = string.Empty;
    private StoreVm storeVm = new();

    private async Task HandleSubmit()
    {
        var result = await storeService.CreateStoreAsync(storeVm.StoreId);
        if (result == null || !result.Success)
        {
            message = string.IsNullOrEmpty(result?.Message) ? "Es ist ein Fehler beim Anlegen der Filiale aufgetreten." : result.Message;
        }
        else
        {
            await OnAdd.InvokeAsync(result.Data);
            CloseModal();
        }
    }

    #region Helper
    public void OpenModal()
    {
        storeVm = new();
        message = string.Empty;
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
