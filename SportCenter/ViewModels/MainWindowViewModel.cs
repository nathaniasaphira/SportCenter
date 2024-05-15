using System.Windows.Input;
using SportCenter.Commands;
using SportCenter.Services.Modals;
using SportCenter.Services.Navigators;
using SportCenter.ViewModels.Factories;

namespace SportCenter.ViewModels;

public sealed class MainWindowViewModel : ViewModelBase
{
    public ViewModelBase CurrentViewModel => _navigator.CurrentViewModel;
    public ViewModelBase? CurrentModalViewModel => _modalNavigator.CurrentViewModel;
    
    public bool IsModalOpen => _modalNavigator.IsModalOpen;

    public ICommand UpdateCurrentViewModelCommand { get; }
    public ICommand UpdateCurrentModalCommand { get; }

    private readonly INavigator _navigator;
    private readonly ModalNavigator _modalNavigator;
    private readonly IModalService _modalService;

    public MainWindowViewModel(INavigator navigator, 
        IViewModelFactory viewModelFactory, 
        IModalService modalService, 
        ModalNavigator modalNavigator)
    {
        _navigator = navigator;
        _modalService = modalService;
        _modalNavigator = modalNavigator;

        _navigator.ViewModelStateChanged += Navigator_ViewModelStateChanged;
        _modalNavigator.ViewModelStateChanged += Navigator_ModalStateChanged;

        UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(_navigator, viewModelFactory);
        UpdateCurrentModalCommand = new UpdateCurrentModalCommand(_modalNavigator, viewModelFactory);

        _modalService.HideModal += ModalService_HideModal;
        _modalService.ShowModal += ModalService_ShowModal;

        InitializeViewModelAndModal();
    }

    public override void Dispose()
    {
        _navigator.ViewModelStateChanged -= Navigator_ViewModelStateChanged;
        _modalNavigator.ViewModelStateChanged -= Navigator_ModalStateChanged;

        _modalService.HideModal -= ModalService_HideModal;
        _modalService.ShowModal -= ModalService_ShowModal;

        base.Dispose();
    }

    private void InitializeViewModelAndModal()
    {
        UpdateCurrentViewModelCommand.Execute(ViewType.Home);
        //TODO (5/16/2024) Nicky: Show login when user is unauthorized.
        _modalService.RaiseShowModal(ModalType.Login);
    }

    #region Service State Events

    private void Navigator_ViewModelStateChanged()
    {
        OnPropertyChanged(nameof(CurrentViewModel));
    }

    private void Navigator_ModalStateChanged()
    {
        OnPropertyChanged(nameof(CurrentModalViewModel));
        OnPropertyChanged(nameof(IsModalOpen));
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