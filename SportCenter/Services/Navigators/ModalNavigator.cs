using SportCenter.ViewModels;

namespace SportCenter.Services.Navigators;

public class ModalNavigator : INavigator
{
    public event Action ViewModelStateChanged = delegate { };

    private ViewModelBase _currentViewModel;

    public ViewModelBase CurrentViewModel
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