using System.Windows.Input;
using SportCenter.Services.Navigators;
using SportCenter.ViewModels.Factories;

namespace SportCenter.Commands;

public class UpdateCurrentViewModelCommand(INavigator navigator, IViewModelFactory viewModelFactory) : ICommand
{
    public event EventHandler? CanExecuteChanged = delegate { };

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        if (parameter is not ViewType viewType)
        {
            return;
        }

        navigator.CurrentViewModel = viewModelFactory.CreateViewModel(viewType);
    }
}