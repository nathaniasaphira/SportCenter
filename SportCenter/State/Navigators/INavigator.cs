using SportCenter.ViewModels;

namespace SportCenter.State.Navigators;

public enum ViewType
{
    Home
}

public enum ModalType
{
    None,
    Login,
    Loading
}

public interface INavigator
{
    ViewModelBase CurrentViewModel { get; set; }
    ViewModelBase? CurrentModal { get; set; }

    event Action ViewModelStateChanged;
    event Action ModalStateChanged;
}