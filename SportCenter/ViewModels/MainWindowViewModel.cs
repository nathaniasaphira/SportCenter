using System.Windows.Input;
using SportCenter.Commands;
using SportCenter.State.Navigators;
using SportCenter.ViewModels.Factories;

namespace SportCenter.ViewModels;

public sealed class MainWindowViewModel : ViewModelBase
{
    public ViewModelBase CurrentViewModel => _navigator.CurrentViewModel;

    public ViewModelBase CurrentModal => _navigator.CurrentModal;

    public ICommand UpdateCurrentViewModelCommand { get; }

    public ICommand UpdateCurrentModalCommand { get; }

    private readonly INavigator _navigator;
    private readonly IViewModelFactory _viewModelFactory;

    public MainWindowViewModel(INavigator navigator, IViewModelFactory viewModelFactory)
    {
        _navigator = navigator;
        _viewModelFactory = viewModelFactory;

        _navigator.ViewModelStateChanged += Navigator_ViewModelStateChanged;
        _navigator.ModalStateChanged += Navigator_ModalStateChanged;

        UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(_navigator, _viewModelFactory);
        UpdateCurrentViewModelCommand.Execute(ViewType.Home);

        UpdateCurrentModalCommand = new UpdateCurrentModalCommand(_navigator, _viewModelFactory);
        UpdateCurrentModalCommand.Execute(ModalType.Login);
    }

    public override void Dispose()
    {
        _navigator.ViewModelStateChanged -= Navigator_ViewModelStateChanged;
        _navigator.ModalStateChanged -= Navigator_ModalStateChanged;

        base.Dispose();
    }

    private void Navigator_ViewModelStateChanged()
    {
        OnPropertyChanged(nameof(CurrentViewModel));
    }

    private void Navigator_ModalStateChanged()
    {
        OnPropertyChanged(nameof(CurrentModal));
    }
}
