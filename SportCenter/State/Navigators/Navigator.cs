using SportCenter.ViewModels;

namespace SportCenter.State.Navigators;

public class Navigator : INavigator
{
    public event Action ViewModelStateChanged = delegate { };
    public event Action ModalStateChanged = delegate { };

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

    private ViewModelBase _currentModal;

    public ViewModelBase CurrentModal
    {
        get => _currentModal;
        set
        {
            _currentModal?.Dispose();

            _currentModal = value;
            ModalStateChanged?.Invoke();
        }
    }

}