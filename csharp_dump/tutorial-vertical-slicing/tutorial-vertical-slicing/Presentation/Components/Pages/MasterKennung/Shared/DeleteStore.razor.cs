using Microsoft.AspNetCore.Components;

namespace Presentation.Components.Pages.MasterKennung.Shared;

public partial class DeleteStore
{
    [Parameter] public EventCallback OnDelete { get; set; }

    private bool showModal = false;
    private string message = string.Empty;
    private string storeId = string.Empty;

    private async Task HandleSubmit()
    {
        var result = await storeService.DeleteStoreAsync(storeId);
        if (result == null || !result.Success || !result.Data)
        {
            message = string.IsNullOrEmpty(result?.Message) ? "Es ist ein Fehler beim Löschen der Filiale aufgetreten." : result.Message;
        }
        else
        {
            await OnDelete.InvokeAsync();
            CloseModal();
        }
    }

    #region Helper
    public void OpenModal(string id)
    {
        message = string.Empty;
        storeId = id;
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
