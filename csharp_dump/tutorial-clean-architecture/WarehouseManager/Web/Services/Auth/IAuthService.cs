namespace Web.Services.Auth;

public interface IAuthService
{
    bool AuthState { get; set; }

    event Action StateChanged;

    void NotifyStateChanged(bool authState);
}