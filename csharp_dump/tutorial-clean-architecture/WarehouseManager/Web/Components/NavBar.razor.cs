namespace Web.Components;

public partial class NavBar
{
    public bool AuthState { get; set; }
    public string Url { get; set; } = string.Empty;

    protected override void OnInitialized()
    {
        AuthState = false;
        AuthService.StateChanged += HandleStateChanged;
        Url = NavigationManager.Uri;
    }

    private void HandleStateChanged()
    {
        AuthState = AuthService.AuthState;
        StateHasChanged();
    }

    protected void NavigateToDashboard()
    {
        NavigationManager.NavigateTo("");
    }

    protected void AddArticle()
    {
        NavigationManager.NavigateTo("add-article");
    }

    protected void Admin()
    {
        NavigationManager.NavigateTo("admin");
    }
}
