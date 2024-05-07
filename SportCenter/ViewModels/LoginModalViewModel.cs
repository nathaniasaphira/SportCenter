using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace SportCenter.ViewModels;

public sealed class LoginModalViewModel : ViewModelBase
{
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
            OnPropertyChanged(nameof(Password));
        }
    }

    public ICommand LoginCommand { get; }

    public LoginModalViewModel()
    {
        _username = string.Empty;
        _password = string.Empty;

        LoginCommand = new RelayCommand(LoginExecute, CanLoginExecute);
    }

    private void LoginExecute()
    {
        // TODO: Implement login logic
    }

    private bool CanLoginExecute()
    {
        return !HasErrors;
    }

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
}
