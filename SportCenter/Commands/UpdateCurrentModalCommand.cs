using System.Windows.Input;
using SportCenter.Services.Modals;
using SportCenter.Services.Navigators;
using SportCenter.ViewModels.Factories;

namespace SportCenter.Commands;

public class UpdateCurrentModalCommand(ModalNavigator modalNavigator, IViewModelFactory viewModelFactory) : ICommand
{
    public event EventHandler? CanExecuteChanged = delegate { };

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        if (parameter is not ModalType modalType)
        {
            return;
        }

        modalNavigator.CurrentViewModel = viewModelFactory.CreateModalViewModel(modalType)!;
    }
}