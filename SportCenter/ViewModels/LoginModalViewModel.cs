using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using SportCenter.Services.Modals;
using SportCenter.Services.Navigators;
using SportCenter.Services.Auth;

namespace SportCenter.ViewModels;

public sealed class LoginModalViewModel : ViewModelBase
{
    private readonly List<(Func<string, bool> rule, string errorMessage)> _usernameRules =
    [
        (string.IsNullOrEmpty, "UsernameRuleRequired"),
        (username => username.Length < 5, "UsernameRuleMin"),
        (username => username.Length > 20, "UsernameRuleMax"),
        (username => username.Contains(' '), "UsernameRuleSpace"),
        (username => username.Contains('@'), "UsernameRuleAt"),
        (username => username.Contains('.'), "UsernameRuleDot")
    ];

    private readonly List<(Func<string, bool> rule, string errorMessage)> _passwordRules =
    [
        (string.IsNullOrEmpty, "PasswordRuleRequired"),
        (password => password.Length < 5, "PasswordRuleMin"),
        (password => password.Length > 20, "PasswordRuleMax")
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

    private readonly IModalService _modalService;
    private readonly LoadingModalViewModel _loadingModalViewModel;
    private readonly IUserService _userService;

    public LoginModalViewModel(IModalService modalService, LoadingModalViewModel loadingModalViewModel, IUserService userService)
    {
        _modalService = modalService;
        _loadingModalViewModel = loadingModalViewModel;
        _userService = userService;

        _username = string.Empty;
        _password = string.Empty;

        LoginCommand = new RelayCommand(LoginExecute, CanLoginExecute);
    }

    #region Login Validation

    private void LoginExecute()
    {
        if (!CanLoginExecute())
        {
            return;
        }

        _modalService.RaiseShowModal(ModalType.Loading);
        _loadingModalViewModel.LoadAuthentication();
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

    #endregion Login Validation
}
