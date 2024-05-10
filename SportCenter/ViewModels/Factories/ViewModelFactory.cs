using SportCenter.State.Navigators;

namespace SportCenter.ViewModels.Factories;

public class ViewModelFactory(
    CreateViewModel<HomeViewModel> createHomeViewModel,
    CreateViewModel<LoginModalViewModel> createLoginModal)
    : IViewModelFactory
{
    public ViewModelBase CreateViewModel(ViewType viewType)
    {
        return viewType switch
        {
            ViewType.Home => createHomeViewModel(),
            _ => throw new ArgumentException("The ViewType does not have a ViewModel.", nameof(viewType))
        };
    }

    public ViewModelBase? CreateModal(ModalType modalType)
    {
        return modalType switch
        {
            ModalType.None => null,
            ModalType.Login => createLoginModal(),
            _ => throw new ArgumentException("The ModalType does not have a ViewModel.", nameof(modalType))
        };
    }
}