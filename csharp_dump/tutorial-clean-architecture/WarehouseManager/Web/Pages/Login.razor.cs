namespace Web.Pages;

public partial class Login
{
    public string Message { get; set; }
    public LoginVm LoginModel { get; set; }

    public Login()
    {
        Message = string.Empty;
        LoginModel = new();
    }

    protected void HandleValidSubmit(string pin)
    {
        if (AppSettings.Value.PIN == pin)
        {
            Message = string.Empty;
            AuthService.NotifyStateChanged(true);
            NavigationManager.NavigateTo("");
        }
        else
        {
            AuthService.NotifyStateChanged(false);
            Message = "Pin is wrong.";
        }
    }
}