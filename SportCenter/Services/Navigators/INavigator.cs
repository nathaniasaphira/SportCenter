using SportCenter.ViewModels;

namespace SportCenter.Services.Navigators;

public enum ViewType
{
    Home
}

public interface INavigator
{
    ViewModelBase CurrentViewModel { get; set; }

    event Action ViewModelStateChanged;
}