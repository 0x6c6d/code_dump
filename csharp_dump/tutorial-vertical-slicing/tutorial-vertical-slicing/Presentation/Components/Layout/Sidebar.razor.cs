using Microsoft.AspNetCore.Components;

namespace Presentation.Components.Layout;

public partial class Sidebar
{
    private bool mkIsActive = false;
    private bool wmIsActive = false;

    protected override void OnInitialized()
    {
        mkIsActive = new Uri(navManager.Uri).AbsolutePath.Contains("/master-kennung") ? true : false;
        wmIsActive = new Uri(navManager.Uri).AbsolutePath.Contains("/warehouse-manager") ? true : false;
    }

    #region Helper
    private void ShowMK()
    {
        mkIsActive = true;
        wmIsActive = false;
        navManager.NavigateTo("/master-kennung");
    }

    private void ShowWM()
    {
        mkIsActive = false;
        wmIsActive = true;
        navManager.NavigateTo("/warehouse-manager");
    }
    #endregion
}
