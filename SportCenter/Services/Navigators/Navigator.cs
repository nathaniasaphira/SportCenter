using SportCenter.ViewModels;

namespace SportCenter.Services.Navigators;

public enum ViewType
{
    Home
}

public class Navigator : INavigator
{
    public event Action ViewModelStateChanged = delegate { };

    private ViewModelBase? _currentViewModel;

    public ViewModelBase? CurrentViewModel
    {
        get => _currentViewModel;
        set
        {
            _currentViewModel?.Dispose();

            _currentViewModel = value;
            ViewModelStateChanged?.Invoke();
        }
    }
}