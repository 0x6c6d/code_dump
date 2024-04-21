namespace Web.Services.Auth;

public class AuthService : IAuthService
{
    public bool AuthState { get; set; }
    public event Action StateChanged = delegate { };

    public void NotifyStateChanged(bool authState)
    {
        AuthState = authState;
        StateChanged?.Invoke();
    }
}
