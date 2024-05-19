using System.Diagnostics;
using System.Windows.Input;
using SportCenter.Commands;
using SportCenter.DataAccess;
using SportCenter.Services.Modals;
using SportCenter.Services.Navigators;
using SportCenter.ViewModels.Factories;

namespace SportCenter.ViewModels;

public sealed class MainWindowViewModel : ViewModelBase
{
    private readonly INavigator _navigator;
    private readonly ModalNavigator _modalNavigator;
    private readonly IModalService _modalService;
    private readonly DatabaseConnection _database;

    public ViewModelBase? CurrentViewModel => _navigator.CurrentViewModel;
    public ViewModelBase? CurrentModalViewModel => _modalNavigator.CurrentViewModel;
    public bool IsModalOpen => _modalNavigator.IsModalOpen;

    public MainWindowViewModel(INavigator navigator, 
        IViewModelFactory viewModelFactory, 
        IModalService modalService, 
        ModalNavigator modalNavigator,
        DatabaseConnection database)
    {
        _navigator = navigator;
        _modalService = modalService;
        _modalNavigator = modalNavigator;

        // TODO: clean up this code
        _database = database;
        _database.CreateConnection();
        List<Models.Entities.Member> members = _database.GetAllData();
        Debug.WriteLine(members[0].Name);

        _navigator.ViewModelStateChanged += Navigator_ViewModelStateChanged;
        _modalNavigator.ViewModelStateChanged += Navigator_ModalStateChanged;

        _navigator.UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(_navigator, viewModelFactory);
        _modalNavigator.UpdateCurrentViewModelCommand = new UpdateCurrentModalCommand(_modalNavigator, viewModelFactory);

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
        _navigator.UpdateCurrentViewModelCommand.Execute(ViewType.Home);

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
        _modalNavigator.UpdateCurrentViewModelCommand.Execute(modalType);
    }

    private void ModalService_HideModal()
    {
        _modalNavigator.UpdateCurrentViewModelCommand.Execute(ModalType.None);
    }

    #endregion Service State Events
}