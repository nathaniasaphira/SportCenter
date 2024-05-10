using SportCenter.State.Navigators;

namespace SportCenter.ViewModels.Factories;

public interface IViewModelFactory
{
    ViewModelBase CreateViewModel(ViewType viewType);

    ViewModelBase? CreateModal(ModalType modalType);
}