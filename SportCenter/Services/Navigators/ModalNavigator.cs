using SportCenter.ViewModels;
using System.Windows.Input;

namespace SportCenter.Services.Navigators;

public enum ModalType
{
    None,
    Login,
    Loading
}

public class ModalNavigator : INavigator
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

    public bool IsModalOpen => CurrentViewModel != null;

    public void CloseModal()
    {
        CurrentViewModel = null;
    }
}