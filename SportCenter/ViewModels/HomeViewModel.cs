using CommunityToolkit.Mvvm.Input;
using SportCenter.State.Modals;
using SportCenter.State.Navigators;
using System.Windows.Input;

namespace SportCenter.ViewModels;

public sealed class HomeViewModel : ViewModelBase
{
    public ICommand DisplayMenuDrawerCommand { get; }

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

    public HomeViewModel(INavigator navigator, IModalService modalService)
    {
        _navigator = navigator;
        _modalService = modalService;

        IsMenuDrawerOpen = false;
        DisplayMenuDrawerCommand = new RelayCommand<bool>(isOpen => IsMenuDrawerOpen = isOpen);
    }
}