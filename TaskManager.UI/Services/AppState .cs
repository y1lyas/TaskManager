using TaskManager.Models;

public class AppState
{
    public bool IsLoggedIn { get; private set; }
    public string? UserEmail { get;  set; }
    public ApplicationUser? CurrentUser { get; set; }



    public event Action? OnChange;

    public void Login(string id, string email, string userName)
    {
        IsLoggedIn = true;
        UserEmail = email;
        NotifyStateChanged();
        CurrentUser = new ApplicationUser
        {
            Id = id,
            Email = email,
            UserName = userName
        };
    }

    public void Logout()
    {
        IsLoggedIn = false;
        UserEmail = null;
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}
