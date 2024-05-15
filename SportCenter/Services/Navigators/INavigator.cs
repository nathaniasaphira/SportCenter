using SportCenter.ViewModels;

namespace SportCenter.Services.Navigators;

public interface INavigator
{
    ViewModelBase? CurrentViewModel { get; set; }

    event Action ViewModelStateChanged;
}