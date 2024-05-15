using SportCenter.Services.Modals;
using SportCenter.Services.Navigators;

namespace SportCenter.ViewModels.Factories;

public interface IViewModelFactory
{
    ViewModelBase? CreateViewModel(ViewType viewType);

    ViewModelBase? CreateModalViewModel(ModalType modalType);
}