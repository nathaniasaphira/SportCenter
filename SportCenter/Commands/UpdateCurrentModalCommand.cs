using System.Windows.Input;
using SportCenter.State.Navigators;
using SportCenter.ViewModels.Factories;

namespace SportCenter.Commands;

public class UpdateCurrentModalCommand(INavigator navigator, IViewModelFactory viewModelFactory) : ICommand
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

        navigator.CurrentModal = viewModelFactory.CreateModal(modalType);
    }
}