namespace Presentation.Components.Pages.WarehouseManager.Shared;

public partial class Header
{
    private void GoToMain()
    {
        navManager.NavigateTo("/warehouse-manager");
    }

    private void GoToAddArticle()
    {
        navManager.NavigateTo("/warehouse-manager/add-article");
    }

    private void GoToSettings()
    {
        navManager.NavigateTo("/warehouse-manager/settings");
    }
}
