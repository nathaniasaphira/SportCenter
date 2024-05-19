using SportCenter.ViewModels;
using System.Windows.Input;

namespace SportCenter.Services.Navigators;

public enum ViewType
{
    Home,
    ServiceTransaction
}

public class Navigator : INavigator
{
    public event Action ViewModelStateChanged = delegate { };

    public ICommand UpdateCurrentViewModelCommand { get; set; }

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