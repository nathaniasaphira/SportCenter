using SportCenter.ViewModels;
using System.Windows.Input;

namespace SportCenter.Services.Navigators;

public interface INavigator
{
    ViewModelBase? CurrentViewModel { get; set; }

    ICommand UpdateCurrentViewModelCommand { get; set; }

    event Action ViewModelStateChanged;
}