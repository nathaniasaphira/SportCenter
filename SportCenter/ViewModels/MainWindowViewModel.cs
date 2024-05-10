using System.Windows.Input;
using SportCenter.Commands;
using SportCenter.State.Modals;
using SportCenter.State.Navigators;
using SportCenter.ViewModels.Factories;

namespace SportCenter.ViewModels;

public sealed class MainWindowViewModel : ViewModelBase
{
    public ViewModelBase CurrentViewModel => _navigator.CurrentViewModel;

    public ViewModelBase? CurrentModal => _navigator.CurrentModal;

    public ICommand UpdateCurrentViewModelCommand { get; }

    public ICommand UpdateCurrentModalCommand { get; }

    private readonly INavigator _navigator;
    private readonly IViewModelFactory _viewModelFactory;
    private readonly IModalService _modalService;

    public MainWindowViewModel(INavigator navigator, IViewModelFactory viewModelFactory, IModalService modalService)
    {
        _navigator = navigator;
        _viewModelFactory = viewModelFactory;
        _modalService = modalService;

        _navigator.ViewModelStateChanged += Navigator_ViewModelStateChanged;
        _navigator.ModalStateChanged += Navigator_ModalStateChanged;

        UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(_navigator, _viewModelFactory);
        UpdateCurrentModalCommand = new UpdateCurrentModalCommand(_navigator, _viewModelFactory);

        _modalService.HideModal += ModalService_HideModal;
        _modalService.ShowModal += ModalService_ShowModal;

        InitializeViewModelAndModal();
    }

    public override void Dispose()
    {
        _navigator.ViewModelStateChanged -= Navigator_ViewModelStateChanged;
        _navigator.ModalStateChanged -= Navigator_ModalStateChanged;

        base.Dispose();
    }

    private void InitializeViewModelAndModal()
    {
        UpdateCurrentViewModelCommand.Execute(ViewType.Home);
        _modalService.RaiseShowModal(ModalType.None);
    }

    #region Service State Events

    private void Navigator_ViewModelStateChanged()
    {
        OnPropertyChanged(nameof(CurrentViewModel));
    }

    private void Navigator_ModalStateChanged()
    {
        OnPropertyChanged(nameof(CurrentModal));
    }

    private void ModalService_ShowModal(ModalType modalType)
    {
        UpdateCurrentModalCommand.Execute(modalType);
    }

    private void ModalService_HideModal()
    {
        UpdateCurrentModalCommand.Execute(ModalType.None);
    }

    #endregion Service State Events
}
