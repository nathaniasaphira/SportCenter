using CommunityToolkit.Mvvm.Input;
using SportCenter.Services.Modals;
using SportCenter.Services.Navigators;
using System.Windows.Input;
using SportCenter.Commands;
using SportCenter.ViewModels.Factories;

namespace SportCenter.ViewModels;

public sealed class HomeViewModel : ViewModelBase
{

    private bool _isMenuDrawerOpen;

    public bool IsMenuDrawerOpen
    {
        get => _isMenuDrawerOpen;
        set
        {
            SetProperty(ref _isMenuDrawerOpen, value);
            OnPropertyChanged(nameof(IsMenuDrawerOpen));
        }
    }

    private readonly INavigator _navigator;
    private readonly IModalService _modalService;

    public ICommand UpdateCurrentViewModelCommand => _navigator.UpdateCurrentViewModelCommand;
    public ICommand DisplayMenuDrawerCommand { get; }

    public HomeViewModel(INavigator navigator, 
        IModalService modalService)
    {
        _navigator = navigator;
        _modalService = modalService;

        IsMenuDrawerOpen = false;
        DisplayMenuDrawerCommand = new RelayCommand<bool>(isOpen => IsMenuDrawerOpen = isOpen);
    }
}