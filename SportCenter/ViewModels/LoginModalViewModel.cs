using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace SportCenter.ViewModels;

public sealed class LoginModalViewModel : ViewModelBase
{
    public static event Action? ShowModalAction = delegate { };
    public static event Action? CloseModalAction = delegate { };

    private readonly List<(Func<string, bool> rule, string errorMessage)> _usernameRules =
    [
        (string.IsNullOrEmpty, "Username is required."),
        (username => username.Length < 5, "Username must be at least 5 characters long."),
        (username => username.Length > 20, "Username must be at most 20 characters long."),
        (username => username.Contains(" "), "Username cannot contain spaces."),
        (username => username.Contains("@"), "Username cannot contain '@'."),
        (username => username.Contains("."), "Username cannot contain '.'.")
    ];

    private readonly List<(Func<string, bool> rule, string errorMessage)> _passwordRules =
    [
        (string.IsNullOrEmpty, "Password is required."),
        (password => password.Length < 5, "Password must be at least 5 characters long."),
        (password => password.Length > 20, "Password must be at most 20 characters long.")
    ];

    private string _username;
    public string Username
    {
        get => _username;
        set
        {
            _username = value;
            ValidateUsername();
        }
    }

    private string _password;
    public string Password
    {
        get => _password;
        set
        {
            _password = value;
            ValidatePassword();
        }
    }

    public ICommand LoginCommand { get; private set; }

    public LoginModalViewModel()
    {
        _username = string.Empty;
        _password = string.Empty;

        LoginCommand = new RelayCommand(LoginExecute, CanLoginExecute);
    }

    public void ShowModal()
    {
        ShowModalAction?.Invoke();
    }

    public void CloseModal()
    {
        CloseModalAction?.Invoke();
    }

    private void LoginExecute()
    {
        if (!CanLoginExecute())
        {
            return;
        }

        CloseModalAction?.Invoke();
    }

    private bool CanLoginExecute()
    {
        return !HasErrors && IsUsernameValid() && IsPasswordValid();
    }

    private bool IsUsernameValid() => GetErrors(nameof(Username)).Equals(string.Empty);

    private bool IsPasswordValid() => GetErrors(nameof(Password)).Equals(string.Empty);

    private void ValidateUsername()
    {
        RemoveError(nameof(Username));

        foreach ((Func<string, bool> rule, string errorMessage) in _usernameRules)
        {
            if (!rule(Username))
            {
                continue;
            }

            AddError(nameof(Username), errorMessage);
        }

        OnPropertyChanged(nameof(Username));
    }

    private void ValidatePassword()
    {
        RemoveError(nameof(Password));

        foreach ((Func<string, bool> rule, string errorMessage) in _passwordRules)
        {
            if (!rule(Password))
            {
                continue;
            }

            AddError(nameof(Password), errorMessage);
        }

        OnPropertyChanged(nameof(Password));
    }
}
